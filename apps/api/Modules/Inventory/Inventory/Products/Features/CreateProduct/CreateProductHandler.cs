using Inventory.Products.Dtos;

namespace Inventory.Products.Features.CreateProduct;

public record CreateProductCommand(ProductDto Product)
    : ICommand<CreateProductResult>;

public record CreateProductResult(Guid Id);

internal class CreateProductHandler(InventoryDbContext dbContext)
    : ICommandHandler<CreateProductCommand, CreateProductResult>
{
    public async Task<CreateProductResult> Handle(CreateProductCommand command, CancellationToken cancellationToken)
    {
        var product = Product.Create(
            Guid.NewGuid(),
            command.Product.Name,
            command.Product.Category,
            command.Product.Price,
            command.Product.Description,
            command.Product.ImageFile,
            command.Product.Stock,
            command.Product.Threshold);

        dbContext.Products.Add(product);
        await dbContext.SaveChangesAsync(cancellationToken);

        return new CreateProductResult(product.Id);
    }
}
