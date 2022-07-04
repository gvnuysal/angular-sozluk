using Microsoft.EntityFrameworkCore;
using Sozluk.Common.ViewModels.Page;

namespace Sozluk.Common.Infrastructure.Extensions;

public static class PagingExtension
{
    public static async Task<PagedViewModel<T>> GetPaged<T>(this IQueryable<T> query, int currentPage, int pageSize) where T:class
    {
        var count = await query.CountAsync();
        Page paging = new(currentPage, pageSize, count);
        var data = await query.Skip(paging.Skip).Take(paging.PageSize).AsNoTracking().ToListAsync();

        var result = new PagedViewModel<T>(data,paging);

        return result;
    }
}