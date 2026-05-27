namespace Inventory.Products.Features.ConsumeProductStock;

public record ConsumeProductStockRequest(int Delta);

public record ConsumeProductStockResponse(Guid Id);
