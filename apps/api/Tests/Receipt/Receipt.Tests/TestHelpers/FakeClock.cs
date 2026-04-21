using Shared.Abstractions.Time;

namespace Receipt.Tests.TestHelpers;

public class FakeClock(DateTime utcNow) : IClock
{
    public DateTime UtcNow { get; set; } = utcNow;
}
