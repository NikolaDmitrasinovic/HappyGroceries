namespace Inventory.Products.Features.ConsumeProductStock;

public record ConsumeProductStockCommand(Guid Id, int Delta)
    : ICommand<ConsumeProductStockResult>;

public record ConsumeProductStockResult(Guid Id);

internal sealed class ConsumeProductStockCommandValidator : IRequestValidator<ConsumeProductStockCommand>
{
    public IReadOnlyCollection<ValidationFailure> Validate(ConsumeProductStockCommand request)
    {
        List<ValidationFailure> failures = [];

        if (request.Id == Guid.Empty)
            failures.Add(new ValidationFailure(nameof(request.Id), "Id is required."));

        if (request.Delta == 0)
            failures.Add(new ValidationFailure(nameof(request.Delta), "Delta cannot be 0."));

        if (request.Delta < 0)
            failures.Add(new ValidationFailure(nameof(request.Delta), "Delta cannot be negative."));

        return failures;
    }
}

internal class ConsumeProductStockHandler(InventoryDbContext dbContext)
    : ICommandHandler<ConsumeProductStockCommand, ConsumeProductStockResult>
{
    public async Task<ConsumeProductStockResult> Handle(ConsumeProductStockCommand command, CancellationToken cancellationToken)
    {
        var product = await dbContext.Products.FindAsync([command.Id], cancellationToken)
                      ?? throw new ProductNotFoundException(command.Id);

        product.ConsumeStock(command.Delta);
        await dbContext.SaveChangesAsync(cancellationToken);

        return new ConsumeProductStockResult(product.Id);
    }
}