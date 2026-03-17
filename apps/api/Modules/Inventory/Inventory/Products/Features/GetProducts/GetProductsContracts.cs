using Shared.Pagination;

namespace Inventory.Products.Features.GetProducts;

public record GetProductsResponse(PaginatedResult<ProductDto> Products);
