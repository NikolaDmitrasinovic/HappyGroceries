namespace Shared.Security;

public interface ICurrentUser
{
    /// <summary>
    /// Stable identifier for auditing. Use "system" when unknown.
    /// </summary>
    string Id { get; }
}
