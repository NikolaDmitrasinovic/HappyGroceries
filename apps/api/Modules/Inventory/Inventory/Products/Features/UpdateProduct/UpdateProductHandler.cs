namespace Inventory.Products.Features.UpdateProduct;

public record UpdateProductCommand(Guid Id, ProductDto ProductDto)
    : ICommand<UpdateProductResult>;

public record UpdateProductResult(Guid Id);

internal class UpdateProductHandler(InventoryDbContext dbContext)
    : ICommandHandler<UpdateProductCommand, UpdateProductResult>
{
    public async Task<UpdateProductResult> Handle(UpdateProductCommand command, CancellationToken cancellationToken)
    {
        var product = await dbContext.Products.FindAsync([command.Id], cancellationToken)
            ?? throw new KeyNotFoundException($"Product with id '{command.Id}' was not found.");

        product.Update(
            command.ProductDto.Name,
            [.. command.ProductDto.Category],
            command.ProductDto.Price,
            command.ProductDto.Description,
            command.ProductDto.ImageFile);

        await dbContext.SaveChangesAsync(cancellationToken);

        return new UpdateProductResult(product.Id);
    }
}
