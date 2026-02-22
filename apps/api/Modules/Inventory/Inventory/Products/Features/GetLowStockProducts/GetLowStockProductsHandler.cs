using Inventory.Products.Queries;

namespace Inventory.Products.Features.GetLowStockProducts;

public record GetLowStockProductsQuery() : IQuery<GetLowStockProductsResult>;

public record GetLowStockProductsResult(IEnumerable<ProductDto> Products);

internal class GetLowStockProductsHandler(InventoryDbContext dbContext)
    : IQueryHandler<GetLowStockProductsQuery, GetLowStockProductsResult>
{
    public async Task<GetLowStockProductsResult> Handle(GetLowStockProductsQuery request, CancellationToken cancellationToken)
    {
        var products = await dbContext.Products
            .AsNoTracking()
            .Where(ProductPredicates.IsLowStockExpression)
            .OrderBy(p => p.Name)
            .ToListAsync(cancellationToken);

        var productDtos = products
            .Select(p => new ProductDto(p.Name, p.Stock, p.Threshold))
            .ToList();

        return new GetLowStockProductsResult(productDtos);
    }
}
