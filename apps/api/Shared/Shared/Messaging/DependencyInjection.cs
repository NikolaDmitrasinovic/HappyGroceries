using Microsoft.Extensions.DependencyInjection;
using Shared.Messaging.Behaviors;

namespace Shared.Messaging;

public static class DependencyInjection
{
    public static IServiceCollection AddMediator(this IServiceCollection services)
    {
        services.AddScoped(typeof(IPipelineBehavior<,>), typeof(LoggingBehavior<,>));
        services.AddScoped(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
        services.AddScoped<IMediator, Mediator>();

        return services;
    }
}
