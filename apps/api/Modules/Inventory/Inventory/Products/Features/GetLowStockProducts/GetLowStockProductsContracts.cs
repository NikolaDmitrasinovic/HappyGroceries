namespace Inventory.Products.Features.GetLowStockProducts;

public record GetLowStockProductsResponse(IEnumerable<ProductDto> Products);
