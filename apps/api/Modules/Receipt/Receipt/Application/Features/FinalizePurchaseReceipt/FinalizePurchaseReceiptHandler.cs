using Shared.Exceptions;

namespace Receipt.Application.Features.FinalizePurchaseReceipt;

internal class FinalizePurchaseReceiptHandler(ReceiptDbContext dbContext) : ICommandHandler<FinalizePurchaseReceiptCommand, FinalizePurchaseReceiptResult>
{
    public async Task<FinalizePurchaseReceiptResult> Handle(FinalizePurchaseReceiptCommand request, CancellationToken cancellationToken)
    {
        var receipt = await dbContext.PurchaseReceipts
            .Include(r => r.Lines)
            .FirstOrDefaultAsync(r => r.Id == request.ReceiptId, cancellationToken)
            ?? throw new NotFoundException($"Purchase receipt with ID {request.ReceiptId} not found.");

        receipt.MarkAsFinalized();

        await dbContext.SaveChangesAsync(cancellationToken);

        return new FinalizePurchaseReceiptResult(receipt.Id);
    }
}
