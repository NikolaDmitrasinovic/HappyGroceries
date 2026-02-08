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
            .Where(p => p.IsLowStock)
            .OrderBy(p => p.Name)
            .ToListAsync(cancellationToken);

        var productDtos = new List<ProductDto>();
        foreach (var product in products)
        {
            var productDto = new ProductDto(product.Name, product.Stock, product.Threshold);
            productDtos.Add(productDto);
        }

        return new GetLowStockProductsResult(productDtos);
    }
}
