using Microsoft.EntityFrameworkCore;
using Net5WebTemplate.Domain.Entities;
using System.Threading;
using System.Threading.Tasks;

namespace Net5WebTemplate.Application.Common.Interfaces
{
    public interface INet5WebTemplateDbContext
    {
        DbSet<Client> Clients { get; set; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}