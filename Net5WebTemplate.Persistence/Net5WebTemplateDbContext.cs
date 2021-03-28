using Microsoft.EntityFrameworkCore;
using Net5WebTemplate.Application.Common.Interfaces;
using Net5WebTemplate.Domain.Entities;
using System.Threading;
using System.Threading.Tasks;

namespace Net5WebTemplate.Persistence
{
    public class Net5WebTemplateDbContext : DbContext, INet5WebTemplateDbContext
    {
        public DbSet<Client> Clients { get; set; }

        public Net5WebTemplateDbContext(DbContextOptions<Net5WebTemplateDbContext> options)
            : base(options)
        {
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            return await base.SaveChangesAsync(cancellationToken);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(Net5WebTemplateDbContext).Assembly);
        }
    }
}