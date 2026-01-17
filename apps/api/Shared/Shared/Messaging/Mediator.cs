namespace Shared.Messaging;

public interface IMediator
{
    Task<TResponse> Send<TResponse>(IRequest<TResponse> request, CancellationToken cancellationToken = default);
    Task Publish(INotification notification, CancellationToken cancellationToken = default);
}

public class Mediator : IMediator
{
    public Task Publish(INotification notification, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task<TResponse> Send<TResponse>(IRequest<TResponse> request, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }
}
