namespace Receipt.Application.Features.OpenPurchaseReceipt;

public record OpenPurchaseReceiptCommand(DateOnly PurchaseDate, string? Location)
    : ICommand<OpenPurchaseReceiptResult>;

public record OpenPurchaseReceiptResult(Guid Id);
