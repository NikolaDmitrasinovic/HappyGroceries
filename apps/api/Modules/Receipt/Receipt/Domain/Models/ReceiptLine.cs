using Shared.DDD;

namespace Receipt.Domain.Models;

public class ReceiptLine : Entity<Guid>
{
    public Guid ReceiptId { get; private set; }
    public string ProductName { get; private set; } = default!;
    public decimal UnitPrice { get; private set; }
    public int Quantity { get; private set; }

    public decimal LineTotal => UnitPrice * Quantity;

    private ReceiptLine() { }
}
