namespace Inventory.Products.Features.CreateProduct;

public record CreateProductCommand(ProductDto Product)
    : ICommand<CreateProductResult>;

public record CreateProductResult(Guid Id);

public sealed class CreateProductCommandValidatior : IRequestValidator<CreateProductCommand>
{
    public IReadOnlyCollection<ValidationFailure> Validate(CreateProductCommand request)
    {
        List<ValidationFailure> failures = [];

        if (String.IsNullOrWhiteSpace(request.Product.Name))
            failures.Add(new ValidationFailure(nameof(request.Product.Name), "Name is required."));

        if (request.Product.Stock < 0)
            failures.Add(new ValidationFailure(nameof(request.Product.Stock), "Stock cannot be negative."));

        if (request.Product.Threshold < 0)
            failures.Add(new ValidationFailure(nameof(request.Product.Threshold), "Threshold cannot be negative."));

        return failures;
    }
}

internal class CreateProductHandler(InventoryDbContext dbContext)
    : ICommandHandler<CreateProductCommand, CreateProductResult>
{
    public async Task<CreateProductResult> Handle(CreateProductCommand command, CancellationToken cancellationToken)
    {
        Product product = CreateNewProduct(command.Product);

        await dbContext.Products.AddAsync(product, cancellationToken);
        await dbContext.SaveChangesAsync(cancellationToken);

        return new CreateProductResult(product.Id);
    }

    private static Product CreateNewProduct(ProductDto productDto)
    {
        return Product.Create(
            productDto.Name,
            productDto.Stock,
            productDto.Threshold);
    }
}
