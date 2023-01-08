using Microsoft.EntityFrameworkCore;

namespace Production
{
    public static class EFExtensions
    {
        public static async Task<PaginationResult<T>> PaginateAsync<T>(this IQueryable<T> items, int page) where T : class
        {
            int countPerPage = 100;
            int count = await items.CountAsync();
            var data = await items.Skip(countPerPage * (page - 1)).Take(countPerPage).ToListAsync();

            return new PaginationResult<T> { TotalCount = count, Data = data };
        }
    }
}
