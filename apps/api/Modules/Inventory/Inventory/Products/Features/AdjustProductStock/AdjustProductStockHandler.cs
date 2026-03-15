namespace Inventory.Products.Features.AdjustProductStock;

public record AdjustProductStockCommand(Guid Id, int Delta)
    : ICommand<AdjustProductStockResult>;

public record AdjustProductStockResult(Guid Id);

internal sealed class AdjustProductStockCommandValidator : IRequestValidator<AdjustProductStockCommand>
{
    public IReadOnlyCollection<ValidationFailure> Validate(AdjustProductStockCommand request)
    {
        List<ValidationFailure> failures = [];

        if (request.Id == Guid.Empty)
            failures.Add(new ValidationFailure(nameof(request.Id), "Id is required."));

        if (request.Delta == 0)
            failures.Add(new ValidationFailure(nameof(request.Delta), "Delta cannot be 0."));

        return failures;
    }
}

internal class AdjustProductStockHandler(InventoryDbContext dbContext)
    : ICommandHandler<AdjustProductStockCommand, AdjustProductStockResult>
{
    public async Task<AdjustProductStockResult> Handle(AdjustProductStockCommand command, CancellationToken cancellationToken)
    {
        var product = await dbContext.Products.FindAsync([command.Id], cancellationToken)
            ?? throw new ProductNotFoundException(command.Id);

        product.AdjustStock(command.Delta);
        await dbContext.SaveChangesAsync(cancellationToken);

        return new AdjustProductStockResult(product.Id);
    }
}
