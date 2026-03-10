namespace Shared.Validation;

public sealed class RequestValidationException(IReadOnlyCollection<ValidationFailure> errors) : Exception("One or more validation failures occurred.")
{
    public IReadOnlyCollection<ValidationFailure> Errors { get; } = errors;
}
