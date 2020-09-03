using Codehub.Data;
using Codehub.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Codehub.Services
{
    public class BootstrapService
    {
        private readonly Guid _userId;

        public BootstrapService(Guid userId)
        {
            _userId = userId;
        }
        public bool CreateBootstrap(BootstrapCreate model)
        {
            var entity =
                new Bootstrap()
                {
                    OwnerId = _userId,
                    Title = model.Title,
                    Content = model.Content,
                    CreatedUtc = DateTimeOffset.Now
                };

            using (var ctx = new ApplicationDbContext())
            {
                ctx.bootstraps.Add(entity);
                return ctx.SaveChanges() == 1;
            }
        }
        public IEnumerable<BootstrapListItem> GetBootstrap()
        {
            using (var ctx = new ApplicationDbContext())
            {
                var query =
                    ctx
                        .bootstraps
                        .Where(e => e.OwnerId == _userId)
                        .Select(
                            e =>
                                new BootstrapListItem
                                {
                                    BootstrapId = e.BootstrapId,
                                    Title = e.Title,
                                    CreatedUtc = e.CreatedUtc
                                }
                        );

                return query.ToArray();
            }
        }
        public IEnumerable<BootstrapListItem> GetAllBootstrap()
        {
            using (var ctx = new ApplicationDbContext())
            {
                var query =
                    ctx
                        .bootstraps
                        .Select(
                            e =>
                                new BootstrapListItem
                                {
                                    BootstrapId = e.BootstrapId,
                                    Title = e.Title,
                                    CreatedUtc = e.CreatedUtc
                                }
                        );

                return query.ToArray();
            }
        }
        public BootstrapDetail GetBootstrapById(int id)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .bootstraps
                        .Single(e => e.BootstrapId == id && e.OwnerId == _userId);
                return
                    new BootstrapDetail
                    {
                        BootstrapId = entity.BootstrapId,
                        Title = entity.Title,
                        Content = entity.Content,
                        CreatedUtc = entity.CreatedUtc,
                        ModifiedUtc = entity.ModifiedUtc
                    };
            }
        } 
        public bool UpdateBootstrap(BootstrapEdit model)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .bootstraps
                        .Single(e => e.BootstrapId == model.BootstrapId && e.OwnerId == _userId);

                entity.Title = model.Title;
                entity.Content = model.Content;
                entity.ModifiedUtc = DateTimeOffset.UtcNow;

                return ctx.SaveChanges() == 1;
            }
        }
        public bool DeleteBootstrap(int codehubId)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                    .bootstraps
                    .Single(e => e.BootstrapId == codehubId && e.OwnerId == _userId);
                ctx.bootstraps.Remove(entity);

                return ctx.SaveChanges() == 1;
            }
        }
    }
}
    
