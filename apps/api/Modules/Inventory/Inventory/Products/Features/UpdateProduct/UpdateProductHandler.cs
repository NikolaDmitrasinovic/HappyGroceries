namespace Inventory.Products.Features.UpdateProduct;

public record UpdateProductCommand(ProductDto ProductDto)
    : ICommand<UpdateProductResult>;

public record UpdateProductResult(ProductDto ProductDto);

internal class UpdateProductHandler
{
}
