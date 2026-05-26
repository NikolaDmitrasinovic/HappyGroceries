using Shared.Exceptions;

namespace Inventory.Products.Exceptions;

public class ProductNotFoundException(Guid id) : NotFoundException($"Product with id '{id}' was not found.");

public class InsufficientStockException(Guid productId, int availableStock, int requestedAmount)
    : Exception($"Product with id '{productId}' has insufficient stock." +
                $" Available stock: {availableStock}, Requested amount: {requestedAmount}.")
{
    public Guid ProductId { get; } =  productId;
    public int AvailableStock { get; } = availableStock;
    public int RequestedAmount { get; } = requestedAmount;
}