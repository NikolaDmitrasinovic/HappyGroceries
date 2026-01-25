namespace Inventory.Products.Features.AdjustProductStock;

public record AdjustProductStockCommand(Guid Id, decimal Delta)
    : ICommand<AdjustProductStockResult>;

public record AdjustProductStockResult(Guid Id);

internal class AdjustProductStockHandler
{
}
