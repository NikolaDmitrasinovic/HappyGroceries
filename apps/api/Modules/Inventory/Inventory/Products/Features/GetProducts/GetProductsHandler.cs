namespace Inventory.Products.Features.GetProducts;

public record GetProductsQuery() : IQuery<GetProductsResult>;

public record GetProductsResult(IEnumerable<ProductDto> Products);

internal class GetProductsHandler
{
}
