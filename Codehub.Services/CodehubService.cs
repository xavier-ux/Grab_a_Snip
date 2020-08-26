using Codehub.Data;
using Codehub.Models;
using System;
using System.Collections.Generic;
using System.Linq;
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
                new Hubs()
                {
                    OwnerId = _userId,
                    Title = model.Title,
                    Content = model.Content,
                    CreatedUtc = DateTimeOffset.Now
                };

            using (var ctx = new ApplicationDbContext())
            {
                ctx.Codehubs.Add(entity);
                return ctx.SaveChanges() == 1;
            }
        }
        public IEnumerable<CodehubListItem> GetCodehub()
        {
            using (var ctx = new ApplicationDbContext())
            {
                var query =
                    ctx
                        .Codehubs
                        .Where(e => e.OwnerId == _userId)
                        .Select(
                            e =>
                                new CodehubListItem
                                {
                                    CodehubId = e.CodehubId,
                                    Title = e.Title,
                                    CreatedUtc = e.CreatedUtc
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
                        .Codehubs
                        .Single(e => e.CodehubId == id && e.OwnerId == _userId);
                return
                    new CodehubDetail
                    {
                        CodehubId = entity.CodehubId,
                        Title = entity.Title,
                        Content = entity.Content,
                        CreatedUtc = entity.CreatedUtc,
                        ModifiedUtc = entity.ModifiedUtc
                    };
            }
        }
        public bool UpdateCodehub (CodehubEdit model)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .Codehubs
                        .Single(e => e.CodehubId == model.CodehubId && e.OwnerId == _userId);

                entity.Title = model.Title;
                entity.Content = model.Content;
                entity.ModifiedUtc = DateTimeOffset.UtcNow;

                return ctx.SaveChanges() == 1;
            }
        }
    }
}
