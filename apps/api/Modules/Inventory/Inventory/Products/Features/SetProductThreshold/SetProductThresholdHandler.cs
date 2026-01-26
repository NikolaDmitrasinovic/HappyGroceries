namespace Inventory.Products.Features.SetProductThreshold;

public record SetProductThresholdCommand(Guid Id, decimal Threshold)
    : ICommand<SetProductThresholdResult>;

public record SetProductThresholdResult(Guid Id);

internal class SetProductThresholdHandler
{
}
