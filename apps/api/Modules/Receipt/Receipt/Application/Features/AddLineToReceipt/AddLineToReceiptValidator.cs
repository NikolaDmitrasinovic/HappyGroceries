namespace Receipt.Application.Features.AddLineToReceipt;

internal class AddLineToReceiptValidator : IRequestValidator<AddLineToReceiptCommand>
{
    public IReadOnlyCollection<ValidationFailure> Validate(AddLineToReceiptCommand request)
    {
        var failures = new List<ValidationFailure>();

        if (string.IsNullOrWhiteSpace(request.ProductName))
            failures.Add(new ValidationFailure(nameof(request.ProductName), "Product name is required."));

        if (request.UnitPrice < 0)
            failures.Add(new ValidationFailure(nameof(request.UnitPrice), "Price cannot be less than 0."));

        if (request.Quantity <= 0)
            failures.Add(new ValidationFailure(nameof(request.Quantity), "Quantity must be greater than 0."));

        return failures;
    }
}
