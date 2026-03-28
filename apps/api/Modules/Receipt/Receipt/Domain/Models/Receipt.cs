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

    public static Receipt Open(DateTime purchaseDate, string? location)
    {
        return new Receipt
        {
            Id = Guid.NewGuid(),
            PurchaseDate = purchaseDate,
            Status = ReceiptStatus.Open,
            TotalAmount = 0,
            Location = location ?? "N/A"
        };
    }

    public void AddLine(ReceiptLine line)
    {
        _lines.Add(line);
        RecalculateTotalAmount();
    }

    public void Complete()
    {
        if (_lines.Count == 0)
            throw new InvalidOperationException("A receipt cannot be finalized without at least one line.");

        Status = ReceiptStatus.Finalized;
    }

    private void RecalculateTotalAmount()
    {
        TotalAmount = _lines.Sum(l => l.LineTotal);
    }
}

public enum ReceiptStatus
{
    Open = 0,
    Finalized = 1
}
