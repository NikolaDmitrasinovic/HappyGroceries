namespace Receipt.Api.Contracts;

public record AddLineToReceiptRequest(Guid ReceiptId, string ProductName, decimal UnitPrice, int Quantity);

public record AddLineToReceiptResponse(Guid ReceiptLineId);
