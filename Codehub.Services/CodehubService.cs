using Codehub.Data;
using Codehub.Models;
using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Codehub.Services
{
    public class CodehubService
    {
        private readonly Guid _userId;

        public CodehubService(Guid userId)
        {
            _userId = userId;
        }
        public bool CreateCodehub(CodehubCreate model)
        {
            var entity =
                new Data.Codehub()
                {
                    OwnerId = _userId,
                    Title = model.Title,
                    Description = model.Description,
                    CreatedUtc = DateTimeOffset.Now
                };

            using (var ctx = new ApplicationDbContext())
            {
                ctx.CodeHubs.Add(entity);
                return ctx.SaveChanges() == 1;
            }
        }
        public IEnumerable<CodehubListItem> GetCodehub()
        {
            using (var ctx = new ApplicationDbContext())
            {
                var query =
                    ctx
                        .CodeHubs
                        .Where(e => e.OwnerId == _userId)
                        .Select(
                            e =>
                                new CodehubListItem
                                {
                                    CodehubId = e.CodehubId,
                                    Title = e.Title,
                                    CreatedUtc = e.CreatedUtc,
                                }
                        );

                return query.ToArray();
            }
        }

        public CodehubDetail GetCodehubById(int id)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .CodeHubs
                        .Single(e => e.CodehubId == id && e.OwnerId == _userId);
                return new CodehubDetail
                {
                    CodehubId = entity.CodehubId,
                    Title = entity.Title,
                    Description = entity.Description
                };
                    

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
        public IEnumerable<CssListItem> GetAllCssByCodeHubId(int Id)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var query =
                    ctx
                        .CodeHubs
                        .Single(e => e.CodehubId == Id && e.OwnerId == _userId)
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
        public bool ConnectCodeWithAHub(int CssId, CodeHubId model)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var Css =
                    ctx
                    .CssCodes
                    .Single(cs => cs.CssId == CssId);

                var Hubs =
                ctx
                .CodeHubs
                .Single(h => h.CodehubId == model.CodehubId);

                Hubs.CssCodes.Add(Css);
                return ctx.SaveChanges() == 1;
            }
        }


        public bool UpdateCodehub(CodehubEdit model)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .CodeHubs
                        .Single(e => e.CodehubId == model.CodehubId && e.OwnerId == _userId);

                entity.Title = model.Title;
                entity.Description = model.Description;
                entity.ModifiedUtc = DateTimeOffset.UtcNow;

                return ctx.SaveChanges() == 1;
            }
        }
        public bool DeleteCodehub(int codehubId)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                    .CodeHubs
                    .Single(e => e.CodehubId == codehubId && e.OwnerId == _userId);
                ctx.CodeHubs.Remove(entity);

                return ctx.SaveChanges() == 1;
            }
        }
    }
}
