namespace Inventory.Products.Features.ReplenishProductStock;

public record ReplenishProductStockRequest(int Delta);

public record ReplenishProductStockResponse(Guid Id);