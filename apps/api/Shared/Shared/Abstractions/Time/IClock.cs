namespace Shared.Abstractions.Time;

public interface IClock
{
    DateTime UtcNow { get; }
}
