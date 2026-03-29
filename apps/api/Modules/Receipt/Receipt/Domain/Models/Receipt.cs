namespace Receipt.Domain.Models;

public class Receipt : Aggregate<Guid>
{
    private readonly List<ReceiptLine> _lines = [];

    public DateOnly PurchaseDate { get; private set; }
    public ReceiptStatus Status { get; private set; }
    public IReadOnlyList<ReceiptLine> Lines => _lines.AsReadOnly();
    public decimal TotalAmount { get; private set; }
    public string Location { get; private set; } = string.Empty;

    private Receipt() { }

    public static Receipt Open(DateOnly purchaseDate, string? location)
    {
        if (purchaseDate > DateOnly.FromDateTime(DateTime.UtcNow))
            throw new ArgumentOutOfRangeException(nameof(purchaseDate), "Purchase date cannot be in the future.");

        return new Receipt
        {
            Id = Guid.NewGuid(),
            PurchaseDate = purchaseDate,
            Status = ReceiptStatus.Open,
            TotalAmount = 0,
            Location = string.IsNullOrWhiteSpace(location) ? "N/A" : location
        };
    }

    public void AddLine(string productName, decimal unitPrice, int quantity)
    {
        if (Status != ReceiptStatus.Open)
            throw new InvalidOperationException("Lines can only be added to an open receipt.");

        _lines.Add(ReceiptLine.Create(Id, productName, unitPrice, quantity));
        RecalculateTotalAmount();
    }

    public void MarkAsFinalized()
    {
        if (Status == ReceiptStatus.Finalized) return;

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
    Open = 1,
    Finalized = 2
}
