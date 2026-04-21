namespace Receipt.Api.Contracts;

public record OpenPurchaseReceiptRequest(DateOnly? PurchaseDate, string? Location);

public record OpenPurchaseReceiptResponse(Guid Id);
