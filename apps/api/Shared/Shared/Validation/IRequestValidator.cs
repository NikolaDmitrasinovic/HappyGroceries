namespace Shared.Validation;

public interface IRequestValidator<in TRequest>
{
    IReadOnlyCollection<ValidationFailure> Validate(TRequest request);
}

public sealed record ValidationFailure(string Property, string Error);
