using Microsoft.Extensions.DependencyInjection;

namespace Shared.Messaging;

public static class DependencyInjection
{
    public static IServiceCollection AddMediator(this IServiceCollection services)
    {
        return services.AddScoped<IMediator, Mediator>();
    }
}
