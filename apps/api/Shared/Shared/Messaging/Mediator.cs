using MediatR;
using Microsoft.Extensions.DependencyInjection;

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

        var handlerType = typeof(IRequestHandler<,>).MakeGenericType(request.GetType(), typeof(TResponse));
        var handler = serviceProvider.GetRequiredService(handlerType);

        var method = handlerType.GetMethod(nameof(IRequestHandler<,>.Handle))!;
        var taskObj = method.Invoke(handler, [request, cancellationToken])!;

        return (Task<TResponse>)taskObj;
    }

    public Task Publish(INotification notification, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }    
}
