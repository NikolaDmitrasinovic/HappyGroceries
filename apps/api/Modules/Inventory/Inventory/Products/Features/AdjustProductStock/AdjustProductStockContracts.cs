namespace Inventory.Products.Features.AdjustProductStock;

public record AdjustProductStockRequest(Guid Id, int Delta);
public record AdjustProductStockResponse(Guid Id);
