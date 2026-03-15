namespace Inventory.Products.Features.UpdateProduct;

public record UpdateProductCommand(Guid Id, ProductDto ProductDto)
    : ICommand<UpdateProductResult>;

public record UpdateProductResult(Guid Id);

internal sealed class UpdateProductCommandValidator : IRequestValidator<UpdateProductCommand>
{
    public IReadOnlyCollection<ValidationFailure> Validate(UpdateProductCommand request)
    {
        List<ValidationFailure> failures = [];

        if (request.Id == Guid.Empty)
            failures.Add(new ValidationFailure(nameof(request.Id), "Id is required."));

        if (String.IsNullOrEmpty(request.ProductDto.Name))
            failures.Add(new ValidationFailure(nameof(request.ProductDto.Name), "New name cannot be empty"));

        return failures;
    }
}

internal class UpdateProductHandler(InventoryDbContext dbContext)
    : ICommandHandler<UpdateProductCommand, UpdateProductResult>
{
    public async Task<UpdateProductResult> Handle(UpdateProductCommand command, CancellationToken cancellationToken)
    {
        var product = await dbContext.Products.FindAsync([command.Id], cancellationToken)
            ?? throw new KeyNotFoundException($"Product with id '{command.Id}' was not found.");

        product.Update(command.ProductDto.Name);
        await dbContext.SaveChangesAsync(cancellationToken);

        return new UpdateProductResult(product.Id);
    }
}
