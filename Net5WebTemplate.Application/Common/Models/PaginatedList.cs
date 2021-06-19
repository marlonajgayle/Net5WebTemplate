using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Net5WebTemplate.Application.Common.Models
{
    public class PaginatedList<T>
    {
        public int Offset { get; }
        public int Limit { get; }
        public int Total { get; }
        public List<T> Items { get; }

        public bool HasPreviousPage => Offset > 1;
        public bool HasNextPage => Offset < Total;

        public PaginatedList(int offset, int limit, int count, List<T> items)
        {
            Offset = offset;
            Limit = limit;
            Total = (int)Math.Ceiling(count / (double)limit);
            Items = items;
        }

        public static async Task<PaginatedList<T>> CreateAsync(IQueryable<T> query, int offset, int limit)
        {
            var count = await query.CountAsync();
            var items = await query.Skip((offset - 1) * limit).Take(limit).ToListAsync();

            return new PaginatedList<T>(offset, limit, count, items);
        }
    }
}
