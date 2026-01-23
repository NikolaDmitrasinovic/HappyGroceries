namespace Inventory.Products.Features.CreateProduct;

public record CreateProductCommand
    : ICommand<CreateProductResult>;

public record CreateProductResult(Guid Id);

internal class CreateProductHandler
{
}
