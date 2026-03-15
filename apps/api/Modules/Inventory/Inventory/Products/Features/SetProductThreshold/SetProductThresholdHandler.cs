namespace Inventory.Products.Features.SetProductThreshold;

public record SetProductThresholdCommand(Guid Id, int Threshold)
    : ICommand<SetProductThresholdResult>;

public record SetProductThresholdResult(Guid Id);

internal sealed class SetProductThresholdCommandValidator : IRequestValidator<SetProductThresholdCommand>
{
    public IReadOnlyCollection<ValidationFailure> Validate(SetProductThresholdCommand request)
    {
        List<ValidationFailure> failures = [];

        if (request.Id == Guid.Empty)
            failures.Add(new ValidationFailure(nameof(request.Id), "Id is required."));

        if (request.Threshold < 0)
            failures.Add(new ValidationFailure(nameof(request.Threshold), "Threshold cannot be negative."));

        return failures;
    }
}

internal class SetProductThresholdHandler(InventoryDbContext dbContext)
    : ICommandHandler<SetProductThresholdCommand, SetProductThresholdResult>
{
    public async Task<SetProductThresholdResult> Handle(SetProductThresholdCommand command, CancellationToken cancellationToken)
    {
        var product = await dbContext.Products.FindAsync([command.Id], cancellationToken)
            ?? throw new ProductNotFoundException(command.Id);

        product.SetThreshold(command.Threshold);
        await dbContext.SaveChangesAsync(cancellationToken);

        return new SetProductThresholdResult(product.Id);
    }
}
