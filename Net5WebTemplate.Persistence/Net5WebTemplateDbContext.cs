using Microsoft.EntityFrameworkCore;
using Net5WebTemplate.Application.Common.Interfaces;
using Net5WebTemplate.Domain.Common;
using Net5WebTemplate.Domain.Entities;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Net5WebTemplate.Persistence
{
    public class Net5WebTemplateDbContext : DbContext, INet5WebTemplateDbContext
    {
        private readonly ICurrentUserService _currentUserService;

        public DbSet<Client> Clients { get; set; }
        public DbSet<LoginAuditLog> LoginAuditLogs { get; set; }
        public DbSet<RefreshToken> RefreshTokens { get; set; }

        public Net5WebTemplateDbContext(DbContextOptions<Net5WebTemplateDbContext> options)
            : base(options)
        {
        }

        public Net5WebTemplateDbContext(
            DbContextOptions<Net5WebTemplateDbContext> options,
            ICurrentUserService currentUserService)
            :base(options)
        {
            _currentUserService = currentUserService;
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            foreach (var entry in ChangeTracker.Entries<AuditableEntity>())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.CreatedBy = _currentUserService.UserId;
                        entry.Entity.Created = DateTime.UtcNow;
                        break;
                    case EntityState.Modified:
                        entry.Entity.LastModifiedBy = _currentUserService.UserId;
                        entry.Entity.LastModified = DateTime.UtcNow;
                        break;
                }

            }

            return await base.SaveChangesAsync(cancellationToken);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(Net5WebTemplateDbContext).Assembly);
        }
    }
}