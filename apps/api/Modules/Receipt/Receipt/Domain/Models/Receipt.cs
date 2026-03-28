using Shared.DDD;

namespace Receipt.Domain.Models;

public class Receipt : Aggregate<Guid>
{
    public DateTime PurchaseDate { get; set; }
    public bool Status { get; set; }
    public decimal TotalAmount { get; set; }
    public string Location { get; set; } = string.Empty;

    private Receipt() { }
}
