namespace Receipt.Api.Contracts;

public record FinalizePurchaseReceiptRequest(Guid ReceiptId);

public record FinalizePurchaseReceiptResponse(Guid ReceiptId);
