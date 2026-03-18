namespace Inventory.Products.Features.GetLowStockProducts;

public record GetLowStockProductsQuery(PaginationRequest PaginationRequest) : IQuery<GetLowStockProductsResult>;

public record GetLowStockProductsResult(PaginatedResult<ProductDto> Products);

internal class GetLowStockProductsHandler(InventoryDbContext dbContext)
    : IQueryHandler<GetLowStockProductsQuery, GetLowStockProductsResult>
{
    public async Task<GetLowStockProductsResult> Handle(GetLowStockProductsQuery query, CancellationToken cancellationToken)
    {
        var pagedProducts = await dbContext.Products
            .AsNoTracking()
            .Where(ProductPredicates.IsLowStockExpression)
            .OrderBy(p => p.Name)
            .Select(p => new ProductDto(p.Name, p.Stock, p.Threshold))
            .ToPaginatedResultAsync(query.PaginationRequest, cancellationToken);

        return new GetLowStockProductsResult(pagedProducts);
    }
}
