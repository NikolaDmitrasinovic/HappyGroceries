namespace Inventory.Products.Features.ReplenishProductStock;

public record ReplenishProductStockCommand(Guid Id, int Delta)
    : ICommand<ReplenishProductStockResult>;

public record ReplenishProductStockResult(Guid Id);

internal sealed class ReplenishProductStockCommandValidator : IRequestValidator<ReplenishProductStockCommand>
{
    public IReadOnlyCollection<ValidationFailure> Validate(ReplenishProductStockCommand request)
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

internal class ReplenishProductStockHandler(InventoryDbContext dbContext)
    : ICommandHandler<ReplenishProductStockCommand, ReplenishProductStockResult>
{
    public async Task<ReplenishProductStockResult> Handle(ReplenishProductStockCommand command, CancellationToken cancellationToken)
    {
        var product = await dbContext.Products.FindAsync([command.Id], cancellationToken)
                      ?? throw new ProductNotFoundException(command.Id);

        product.ReplenishStock(command.Delta);
        await dbContext.SaveChangesAsync(cancellationToken);

        return new ReplenishProductStockResult(product.Id);
    }
}