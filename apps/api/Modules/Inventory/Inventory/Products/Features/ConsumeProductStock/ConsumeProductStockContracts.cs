namespace Inventory.Products.Features.ConsumeProductStock;

public record ConsumeProductStockRequest(Guid Id, int Delta);

public record ConsumeProductStockResponse(Guid Id);