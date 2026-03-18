namespace Inventory.Products.Features.GetProducts;

public record GetProductsQuery(PaginationRequest PaginationRequest) : IQuery<GetProductsResult>;

public record GetProductsResult(PaginatedResult<ProductDto> Products);

internal class GetProductsHandler(InventoryDbContext dbContext)
    : IQueryHandler<GetProductsQuery, GetProductsResult>
{
    public async Task<GetProductsResult> Handle(GetProductsQuery query, CancellationToken cancellationToken)
    {
        var pagedProducts = await dbContext.Products
            .AsNoTracking()
            .OrderBy(p => p.Name)
            .Select(p => new ProductDto(p.Name, p.Stock, p.Threshold))
            .ToPaginatedResultAsync(query.PaginationRequest, cancellationToken);

        return new GetProductsResult(pagedProducts);
    }
}
