namespace Receipt.Api.Contracts;

public record OpenPurchaseReceiptRequest(PurchaseReceiptDto PurchaseReceipt);

public record OpenPurchaseReceiptResponse(Guid Id);
