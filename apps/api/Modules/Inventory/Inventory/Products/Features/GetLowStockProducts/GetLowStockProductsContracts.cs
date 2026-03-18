namespace Inventory.Products.Features.GetLowStockProducts;

public record GetLowStockProductsResponse(PaginatedResult<ProductDto> Products);
