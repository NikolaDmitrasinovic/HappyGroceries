using Shared.Exceptions;

namespace Receipt.Application.Features.AddLineToReceipt;

internal class AddLineToReceiptHandler(ReceiptDbContext dbContext) : IRequestHandler<AddLineToReceiptCommand, AddLineToReceiptResult>
{
    public async Task<AddLineToReceiptResult> Handle(AddLineToReceiptCommand request, CancellationToken cancellationToken)
    {
        var _ = await dbContext.PurchaseReceipts.FindAsync([request.ReceiptId], cancellationToken) ?? throw new NotFoundException($"Receipt with id {request.ReceiptId} not found.");

        var receiptLine = ReceiptLine.Create(request.ReceiptId, request.ProductName, request.UnitPrice, request.Quantity);

        await dbContext.ReceiptLines.AddAsync(receiptLine, cancellationToken);
        await dbContext.SaveChangesAsync(cancellationToken);

        return new AddLineToReceiptResult(receiptLine.Id);
    }
}
