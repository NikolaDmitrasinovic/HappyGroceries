using Shared.DDD;

namespace Receipt.Domain.Models;

public class Receipt : Aggregate<Guid>
{
    private Receipt() { }
}
