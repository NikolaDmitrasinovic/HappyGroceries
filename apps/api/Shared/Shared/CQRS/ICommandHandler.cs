using Shared.Messaging;

namespace Shared.CQRS;

public interface ICommandHandler<in TCommand>
    : ICommandHanler<TCommand, Unit>
    where TCommand : ICommand<Unit>
{
}

public interface ICommandHanler<in TCommand, TResponse>
    : IRequestHandler<TCommand, TResponse>
    where TCommand : ICommand<TResponse>
    where TResponse : notnull
{
}
