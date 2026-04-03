namespace Receipt.Application.Features.OpenPurchaseReceipt;

public record OpenPurchaseReceiptCommand(PurchaseReceiptDto PurchaseReceipt)
    : ICommand<OpenPurchaseReceiptResult>;

public record OpenPurchaseReceiptResult(Guid Id);
