using Shared.DDD;

namespace Receipt.Domain.Models;

public class Receipt : Aggregate<Guid>
{
    public DateTime Date { get; set; }
    public string Location { get; set; } = string.Empty;

    private Receipt() { }
}
