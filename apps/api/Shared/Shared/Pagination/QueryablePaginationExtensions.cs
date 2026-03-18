using Microsoft.EntityFrameworkCore;

namespace Shared.Pagination;

public static class QueryablePaginationExtensions
{
    public static async Task<PaginatedResult<T>> ToPaginatedResultAsync<T>(
        this IQueryable<T> query,
        PaginationRequest request,
        CancellationToken cancellationToken = default)
        where T : class
    {
        var pageIndex = request.PageIndex;
        var pageSize = request.PageSize;

        var totalCount = await query.LongCountAsync(cancellationToken);

        var items = await query
            .Skip(pageIndex * pageSize)
            .Take(pageSize)
            .ToListAsync(cancellationToken);

        return new PaginatedResult<T>(
            pageIndex,
            pageSize,
            totalCount,
            items);
    }
}
