using Shared.DDD;

namespace Receipt.Domain.Models;

public class ReceiptLine : Entity<Guid>
{
    public string ProductName { get; set; } = default!;
    public decimal UnitPrice { get; set; }
    public int Quantity { get; set; }

    public decimal LineTotal => UnitPrice * Quantity;

    private ReceiptLine() { }
}
