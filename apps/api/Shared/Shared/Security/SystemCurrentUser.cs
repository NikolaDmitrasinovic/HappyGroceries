namespace Shared.Security;

public sealed class SystemCurrentUser : ICurrentUser
{
    public string Id => "system";
}
