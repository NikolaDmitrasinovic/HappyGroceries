namespace Inventory.Products.Features.GetLowStockProducts;

public record GetLowStockProductQuery() : IQuery<GetLowStockProductsResult>;

public record GetLowStockProductsResult(IEnumerable<ProductDto> Products);

internal class GetLowStockProductsHandler
{
}
