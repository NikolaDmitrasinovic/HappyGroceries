namespace Receipt.Domain.Models;

public class Receipt : Aggregate<Guid>
{
    private readonly List<ReceiptLine> _lines = [];

    public DateTime PurchaseDate { get; private set; }
    public ReceiptStatus Status { get; private set; }
    public IReadOnlyList<ReceiptLine> Lines => _lines.AsReadOnly();
    public decimal TotalAmount { get; private set; }
    public string Location { get; private set; } = string.Empty;

    private Receipt() { }

    private void RecalculateTotalAmount()
    {
        TotalAmount = _lines.Sum(l => l.LineTotal);
    }
}

public enum ReceiptStatus
{
    Open = 0,
    Finalizes = 1
}
