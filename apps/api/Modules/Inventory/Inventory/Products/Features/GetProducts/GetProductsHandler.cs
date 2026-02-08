namespace Inventory.Products.Features.GetProducts;

public record GetProductsQuery() : IQuery<GetProductsResult>;

public record GetProductsResult(IEnumerable<ProductDto> Products);

internal class GetProductsHandler(InventoryDbContext dbContext)
    : IQueryHandler<GetProductsQuery, GetProductsResult>
{
    public async Task<GetProductsResult> Handle(GetProductsQuery query, CancellationToken cancellationToken)
    {
        var products = await dbContext.Products
            .AsNoTracking()
            .OrderBy(p => p.Name)
            .ToListAsync(cancellationToken);

        var productDtos = new List<ProductDto>();
        foreach (var product in products)
        {
            var productDto = new ProductDto(product.Name, product.Stock, product.Threshold);
            productDtos.Add(productDto);
        }

        return new GetProductsResult(productDtos);
    }
}
