namespace Inventory.Products.Features.UpdateProduct;

public record UpdateProductCommand(string Id, ProductDto ProductDto)
    : ICommand<UpdateProductResult>;

public record UpdateProductResult(ProductDto ProductDto);

internal class UpdateProductHandler(InventoryDbContext dbContext)
    : ICommandHandler<UpdateProductCommand, UpdateProductResult>
{
    public async Task<UpdateProductResult> Handle(UpdateProductCommand command, CancellationToken cancellationToken)
    {
        var product = await dbContext.Products.FirstOrDefaultAsync(p => p.Id.ToString() == command.Id) 
            ?? throw new ArgumentNullException($"Product with {command.Id} can not be found");

        product.Update(
            command.ProductDto.Name,
            command.ProductDto.Category,
            command.ProductDto.Price,
            command.ProductDto.Description,
            command.ProductDto.ImageFile);

        await dbContext.SaveChangesAsync(cancellationToken);

        var productDto = new ProductDto(
            product.Name,
            product.Category,
            product.Description,
            product.ImageFile,
            product.Price,
            product.Stock,
            product.Threshold);

        return new UpdateProductResult(productDto);
    }
}
