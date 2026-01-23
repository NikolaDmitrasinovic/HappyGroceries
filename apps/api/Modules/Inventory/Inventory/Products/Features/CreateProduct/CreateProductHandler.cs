namespace Inventory.Products.Features.CreateProduct;

public record CreateProductCommand(ProductDto Product)
    : ICommand<CreateProductResult>;

public record CreateProductResult(Guid Id);

internal class CreateProductHandler(InventoryDbContext dbContext)
    : ICommandHandler<CreateProductCommand, CreateProductResult>
{
    public async Task<CreateProductResult> Handle(CreateProductCommand command, CancellationToken cancellationToken)
    {
        Product product = CreateNewPRoduct(command.Product);

        dbContext.Products.Add(product);
        await dbContext.SaveChangesAsync(cancellationToken);

        return new CreateProductResult(product.Id);
    }

    private static Product CreateNewPRoduct(ProductDto productDto)
    {
        return Product.Create(
            Guid.NewGuid(),
            productDto.Name,
            productDto.Category,
            productDto.Price,
            productDto.Description,
            productDto.ImageFile,
            productDto.Stock,
            productDto.Threshold);
    }
}
