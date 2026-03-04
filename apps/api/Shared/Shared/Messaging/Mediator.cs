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
    private static readonly string HandleMethodName = nameof(IRequestHandler<,>.Handle);
    private readonly IServiceProvider _serviceProvider = serviceProvider ?? throw new ArgumentNullException(nameof(serviceProvider));

    public Task<TResponse> Send<TResponse>(IRequest<TResponse> request, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(request);

        var requstType = request.GetType();
        var handlerType = typeof(IRequestHandler<,>).MakeGenericType(requstType, typeof(TResponse));

        object handler;
        try
        {
            handler = _serviceProvider.GetRequiredService(handlerType);
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

        RequestHandlerDelegate<TResponse> next = () =>
        {
            object? taskObject;
            try
            {
                taskObject = handleMethod.Invoke(handler, [request, cancellationToken]);
            }
            catch (TargetInvocationException ex) when (ex.InnerException is not null)
            {
                throw ex.InnerException;
            }

            if (taskObject is not Task<TResponse> task)
                throw new InvalidOperationException($"Hanlder '{handlerType.FullName}' retruned unexpected type. Expected Task<{typeof(TResponse).Name}>.");

            return task;
        };

        var behaviorInterfaceType = typeof(IPipelineBehavior<,>).MakeGenericType(requstType, typeof(TResponse));
        var behaviors = _serviceProvider.GetServices(behaviorInterfaceType);

        foreach (var behavior in behaviors.Reverse())
        {
            if (behavior is null)
                throw new InvalidOperationException($"DI returned a null pipeline behavior for '{behaviorInterfaceType.FullName}'.");

            var behaviorHandleMethod = GetHandleMethodOrThrow(behaviorInterfaceType);

            var currentNext = next;

            next = () =>
            {
                object? taskObject;
                try
                {
                    taskObject = behaviorHandleMethod.Invoke(
                        behavior,
                        [request, cancellationToken, currentNext]);
                }
                catch (TargetInvocationException ex) when (ex.InnerException is not null)
                {
                    throw ex.InnerException;
                }

                if (taskObject is not Task<TResponse> task)
                    throw new InvalidOperationException($"Pipeline behavior '{behavior.GetType().FullName}' returned unexpected type. Expected Task<{typeof(TResponse).Name}>.");

                return task;
            };
        }

        return next();
    }

    public async Task Publish(INotification notification, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(notification);

        var notificationType = notification.GetType();
        var handlerType = typeof(INotificationHandler<>).MakeGenericType(notificationType);

        var handlers = _serviceProvider.GetServices(handlerType);

        var handleMethod = GetHandleMethodOrThrow(handlerType);

        foreach (var handler in handlers)
        {
            if (handler is null) throw new InvalidOperationException(
                $"DI returned a null handler instance for '{handlerType.FullName}'.");

            object? taskObj;
            try
            {
                taskObj = handleMethod.Invoke(handler, [notification, cancellationToken]);
            }
            catch (TargetInvocationException tie) when (tie.InnerException is not null)
            {

                throw tie.InnerException;
            }

            if (taskObj is not Task task)
                throw new InvalidOperationException(
                    $"Notification handler '{handler.GetType().FullName}' returned unexpected type. " +
                    "Expected Task.");

            await task.ConfigureAwait(false);
        }
    }

    private static MethodInfo GetHandleMethodOrThrow(Type handlerInterfaceType)
    {
        return handlerInterfaceType.GetMethod(HandleMethodName)
            ?? throw new InvalidOperationException(
                $"Could not find '{HandleMethodName}' method on '{handlerInterfaceType.FullName}'.");
    }
}
