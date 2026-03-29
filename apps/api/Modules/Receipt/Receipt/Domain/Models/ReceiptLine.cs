namespace Receipt.Domain.Models;

public class ReceiptLine : Entity<Guid>
{
    public Guid ReceiptId { get; private set; }
    public string ProductName { get; private set; } = default!;
    public decimal UnitPrice { get; private set; }
    public int Quantity { get; private set; }

    public decimal LineTotal => UnitPrice * Quantity;

    private ReceiptLine() { }

    public static ReceiptLine Create(Guid receiptId, string productName,  decimal unitPrice, int quantity)
    {
        if (receiptId == Guid.Empty)
            throw new ArgumentException("Receipt id cannot be empty.", nameof(receiptId));

        ArgumentException.ThrowIfNullOrWhiteSpace(productName, nameof(productName));
        ArgumentOutOfRangeException.ThrowIfNegative(unitPrice, nameof(unitPrice));
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(quantity, nameof(quantity));

        return new ReceiptLine
        {
            Id = Guid.NewGuid(),
            ReceiptId = receiptId,
            ProductName = productName,
            UnitPrice = unitPrice,
            Quantity = quantity
        };
    }
}
