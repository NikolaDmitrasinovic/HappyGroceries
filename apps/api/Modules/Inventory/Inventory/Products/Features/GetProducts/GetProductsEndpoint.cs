namespace Inventory.Products.Features.GetProducts;

public record GetProductsResponse(IEnumerable<ProductDto> Products);


internal class GetProductsEndpoint
{
}
