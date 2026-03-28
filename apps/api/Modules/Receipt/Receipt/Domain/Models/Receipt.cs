using Shared.DDD;

namespace Receipt.Domain.Models;

public class Receipt : Aggregate<Guid>
{
    private readonly List<ReceiptLine> _lines = [];

    public DateTime PurchaseDate { get; set; }
    public bool Status { get; set; }
    public IReadOnlyList<ReceiptLine> Lines => _lines.AsReadOnly();
    public decimal TotalAmount => Lines.Sum(l => l.LineTotal);
    public string Location { get; set; } = string.Empty;

    private Receipt() { }
}
