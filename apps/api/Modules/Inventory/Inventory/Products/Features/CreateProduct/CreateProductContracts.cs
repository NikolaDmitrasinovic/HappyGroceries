namespace Inventory.Products.Features.CreateProduct;

public record CreateProductRequest(ProductDto Product);
public record CreateProductResponse(Guid Id);
