namespace Inventory.Products.Features.AdjustProductStock;

public record AdjustProductStockCommand(Guid Id, decimal Delta)
    : ICommand<AdjustProductStockResult>;

public record AdjustProductStockResult(Guid Id);

internal class AdjustProductStockHandler(InventoryDbContext dbContext)
    : ICommandHandler<AdjustProductStockCommand, AdjustProductStockResult>
{
    public async Task<AdjustProductStockResult> Handle(AdjustProductStockCommand command, CancellationToken cancellationToken)
    {
        var product = await dbContext.Products.FindAsync([command.Id], cancellationToken)
            ?? throw new KeyNotFoundException($"Product with id '{command.Id}' was not found.");

        product.AdjustStock(command.Delta);
        await dbContext.SaveChangesAsync(cancellationToken);

        return new AdjustProductStockResult(product.Id);
    }
}
