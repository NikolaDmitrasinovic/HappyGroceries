using Shared.Exceptions;

namespace Receipt.Application.Features.AddLineToReceipt;

internal class AddLineToReceiptHandler(ReceiptDbContext dbContext) : ICommandHandler<AddLineToReceiptCommand, AddLineToReceiptResult>
{
    public async Task<AddLineToReceiptResult> Handle(AddLineToReceiptCommand request, CancellationToken cancellationToken)
    {
        var receipt = await dbContext.PurchaseReceipts
            .Include(r => r.Lines)
            .FirstOrDefaultAsync(r => r.Id == request.ReceiptId, cancellationToken)
            ?? throw new NotFoundException($"Receipt with id {request.ReceiptId} not found.");

        receipt.AddLine(request.ProductId, request.ProductName, request.UnitPrice, request.Quantity);

        var addedLine = receipt.Lines[receipt.Lines.Count - 1];

        await dbContext.SaveChangesAsync(cancellationToken);

        return new AddLineToReceiptResult(addedLine.Id);
    }
}
