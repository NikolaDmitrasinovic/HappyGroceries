using Shared.Messaging;

namespace Shared.Validation;

public sealed class ValidationBehavior<TRequest, TResponse>(IEnumerable<IRequestValidator<TRequest>> validators)
    : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
{
    private readonly IEnumerable<IRequestValidator<TRequest>> _validators = validators;

    public Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
    {
        var failures = _validators
            .SelectMany(v => v.Validate(request))
            .ToArray();

        if (failures.Length != 0)
            throw new RequestValidationException(failures);

        return next();
    }
}
