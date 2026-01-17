using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Shared.Messaging;

public interface IMediator
{
    Task<TResponse> Send<TResponse>(IRequest<TResponse> request, CancellationToken cancellationToken = default);
    Task Publish(INotification notification, CancellationToken cancellationToken = default);
}

public class Mediator(IServiceProvider serviceProvider) : IMediator
{
    public Task<TResponse> Send<TResponse>(IRequest<TResponse> request, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(request);

        var requstType = request.GetType();
        var handlerType = typeof(IRequestHandler<,>).MakeGenericType(requstType, typeof(TResponse));

        object handler;
        try
        {
            handler = serviceProvider.GetRequiredService(handlerType);
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException(
                $"No handler is registerd for request '{requstType.FullName}' " +
                $"with response '{typeof(TResponse).FullName}'. " +
                $"Expected DI registration for '{handlerType.FullName}'.",
                ex);
        }

        var handleMethod = GetHandleMethodOrThrow(handlerType);

        var method = handlerType.GetMethod(nameof(IRequestHandler<,>.Handle))!;
        var taskObj = method.Invoke(handler, [request, cancellationToken])!;

        return (Task<TResponse>)taskObj;
    }
        
    public Task Publish(INotification notification, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    private static MethodInfo GetHandleMethodOrThrow(Type handlerInterfaceType)
    {
        return handlerInterfaceType.GetMethod("Handle")
            ?? throw new InvalidOperationException(
                $"Could not find 'Handle' method on '{handlerInterfaceType.FullName}'.");
    }
}
