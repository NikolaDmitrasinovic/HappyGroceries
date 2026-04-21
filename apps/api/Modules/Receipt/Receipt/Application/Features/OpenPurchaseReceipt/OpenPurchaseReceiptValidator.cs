using Shared.Abstractions.Time;

namespace Receipt.Application.Features.OpenPurchaseReceipt;

internal class OpenPurchaseReceiptValidator(IClock clock) : IRequestValidator<OpenPurchaseReceiptCommand>
{
    private readonly IClock _clock = clock;

    public IReadOnlyCollection<ValidationFailure> Validate(OpenPurchaseReceiptCommand request)
    {
        List<ValidationFailure> failures = [];

        var today = DateOnly.FromDateTime(_clock.UtcNow);
        var minimumDate = today.AddYears(-10);

        if (request.PurchaseDate > today)
            failures.Add(new ValidationFailure(nameof(request.PurchaseDate), "Purchase date cannot be in the future."));

        if (request.PurchaseDate < minimumDate)
            failures.Add(new ValidationFailure(nameof(request.PurchaseDate), "Purchase date is too old."));

        return failures;
    }
}
