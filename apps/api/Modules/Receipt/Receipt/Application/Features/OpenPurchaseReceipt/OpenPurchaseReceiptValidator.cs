using Shared.Abstractions.Time;

namespace Receipt.Application.Features.OpenPurchaseReceipt;

internal class OpenPurchaseReceiptValidator(IClock clock) : IRequestValidator<OpenPurchaseReceiptCommand>
{
    private readonly IClock _clock = clock;
    public IReadOnlyCollection<ValidationFailure> Validate(OpenPurchaseReceiptCommand request)
    {
        List<ValidationFailure> failures = [];

        if (request.PurchaseReceipt.PurchaseDate > DateOnly.FromDateTime(_clock.UtcNow))
            failures.Add(new ValidationFailure(nameof(request.PurchaseReceipt.PurchaseDate), "Purchase date cannot be in the future."));

        return failures;
    }
}
