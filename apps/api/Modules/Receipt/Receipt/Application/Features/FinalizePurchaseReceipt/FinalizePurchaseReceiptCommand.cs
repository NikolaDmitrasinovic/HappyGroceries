namespace Receipt.Application.Features.FinalizePurchaseReceipt;

public record FinalizePurchaseReceiptCommand(Guid ReceiptId) : ICommand<FinalizePurchaseReceiptResult>;

public record FinalizePurchaseReceiptResult(Guid ReceiptId);
