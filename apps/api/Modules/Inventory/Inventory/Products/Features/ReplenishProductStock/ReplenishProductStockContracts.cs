namespace Inventory.Products.Features.ReplenishProductStock;

public record ReplenishProductStockRequest(Guid Id, int Delta);

public record ReplenishProductStockResponse(Guid Id);