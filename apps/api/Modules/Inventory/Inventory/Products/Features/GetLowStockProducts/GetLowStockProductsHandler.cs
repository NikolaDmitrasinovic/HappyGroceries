namespace Inventory.Products.Features.GetLowStockProducts;

public record GetLowStockProductsQuery(PaginationRequest PaginationRequest) : IQuery<GetLowStockProductsResult>;

public record GetLowStockProductsResult(PaginatedResult<ProductDto> Products);

internal class GetLowStockProductsHandler(InventoryDbContext dbContext)
    : IQueryHandler<GetLowStockProductsQuery, GetLowStockProductsResult>
{
    public async Task<GetLowStockProductsResult> Handle(GetLowStockProductsQuery query, CancellationToken cancellationToken)
    {
        var pageIndex = query.PaginationRequest.PageIndex;
        var pageSize = query.PaginationRequest.PageSize;

        var totalCount = await dbContext.Products.LongCountAsync(cancellationToken);

        var products = await dbContext.Products
            .AsNoTracking()
            .Where(ProductPredicates.IsLowStockExpression)
            .OrderBy(p => p.Name)
            .ToListAsync(cancellationToken);

        var productDtos = products
            .Select(p => new ProductDto(p.Name, p.Stock, p.Threshold))
            .ToList();

        return new GetLowStockProductsResult(
            new PaginatedResult<ProductDto>(
                pageIndex,
                pageSize,
                totalCount,
                productDtos)
            );
    }
}
