namespace Receipt.Application.Features.OpenPurchaseReceipt;

internal class OpenPurchaseReceiptHandler(ReceiptDbContext dbContext) : ICommandHandler<OpenPurchaseReceiptCommand, OpenPurchaseReceiptResult>
{
    public async Task<OpenPurchaseReceiptResult> Handle(OpenPurchaseReceiptCommand request, CancellationToken cancellationToken)
    {
        PurchaseReceipt purchaseReceipt = CreateOpenReceipt(request.PurchaseReceipt);

        await dbContext.PurchaseReceipts.AddAsync(purchaseReceipt, cancellationToken);
        await dbContext.SaveChangesAsync(cancellationToken);

        return new OpenPurchaseReceiptResult(purchaseReceipt.Id);
    }

    private static PurchaseReceipt CreateOpenReceipt(PurchaseReceiptDto request)
    {
        return PurchaseReceipt.Open(
            request.PurchaseDate,
            request.Location);
    }
}
