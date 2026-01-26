namespace Inventory.Products.Features.SetProductThreshold;

public record SetProductThresholdCommand(Guid Id, int Threshold)
    : ICommand<SetProductThresholdResult>;

public record SetProductThresholdResult(Guid Id);

internal class SetProductThresholdHandler(InventoryDbContext dbContext)
    : ICommandHandler<SetProductThresholdCommand, SetProductThresholdResult>
{
    public async Task<SetProductThresholdResult> Handle(SetProductThresholdCommand command, CancellationToken cancellationToken)
    {
        var product = await dbContext.Products.FindAsync([command.Id], cancellationToken)
            ?? throw new KeyNotFoundException($"Product with id '{command.Id}' was not found.");

        product.SetThreshold(command.Threshold);
        await dbContext.SaveChangesAsync(cancellationToken);

        return new SetProductThresholdResult(product.Id);
    }
}
