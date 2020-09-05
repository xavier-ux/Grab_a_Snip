using Codehub.Data;
using Codehub.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Codehub.Services
{
    public class CssService
    {
        private readonly Guid _userId;

        public CssService(Guid userId)
        {
            _userId = userId;
        }
        
        public bool CreateCss(CssCreate model)
        {
            var entity =
                new CssCode()
                {
                    OwnerId = _userId,
                    Title = model.Title,
                    Content = model.Content,
                    CreatedUtc = DateTimeOffset.Now
                };

            using (var ctx = new ApplicationDbContext())
            {
                ctx.CssCodes.Add(entity);
                return ctx.SaveChanges() == 1;
            }
        }
        public IEnumerable<CssListItem> GetCss()
        {
            using (var ctx = new ApplicationDbContext())
            {
                var query =
                    ctx
                        .CssCodes
                        .Where(e => e.OwnerId == _userId)
                        .Select(
                            e =>
                                new CssListItem
                                {
                                    CssId = e.CssId,
                                    Title = e.Title,
                                    CreatedUtc = e.CreatedUtc
                                }
                        );

                return query.ToArray();
            }
        }
        public IEnumerable<CssListItem> GetAllCss()
        {
            using (var ctx = new ApplicationDbContext())
            {
                var query =
                    ctx
                        .CssCodes
                        .Select(
                            e =>
                                new CssListItem
                                {
                                    CssId = e.CssId,
                                    Title = e.Title,
                                    CreatedUtc = e.CreatedUtc
                                }
                        );

                return query.ToArray();
            }
        }
        public CssDetail GetCssById(int id)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .CssCodes
                        .Single(e => e.CssId == id && e.OwnerId == _userId);
                return
                    new CssDetail
                    {
                        CssId = entity.CssId,
                        Title = entity.Title,
                        Content = entity.Content,
                        CreatedUtc = entity.CreatedUtc,
                        ModifiedUtc = entity.ModifiedUtc
                    };
            }
        }
        public bool UpdateCss(CssEdit model)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .CssCodes
                        .Single(e => e.CssId == model.CssId && e.OwnerId == _userId);

                entity.Title = model.Title;
                entity.Content = model.Content;
                entity.ModifiedUtc = DateTimeOffset.UtcNow;

                return ctx.SaveChanges() == 1;
            }
        }
        public bool DeleteCss(int CssId)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                    .CssCodes
                    .Single(e => e.CssId == CssId && e.OwnerId == _userId);
                ctx.CssCodes.Remove(entity);

                return ctx.SaveChanges() == 1;
            }
        }
    }
}
