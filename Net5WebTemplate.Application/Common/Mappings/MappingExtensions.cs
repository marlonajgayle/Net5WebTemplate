using Net5WebTemplate.Application.Common.Models;
using System.Linq;
using System.Threading.Tasks;

namespace Net5WebTemplate.Application.Common.Mappings
{
    public static class MappingExtensions
    {
        public static Task<PaginatedList<TDestination>> PaginatedListAsync<TDestination>(this IQueryable<TDestination> queryable,
            int offset, int limit)
        {
            return PaginatedList<TDestination>.CreateAsync(queryable, offset, limit);
        }
    }
}
