namespace Receipt.Api.Contracts;

public record AddLineToReceiptRequest(string ProductName, decimal UnitPrice, int Quantity);

public record AddLineToReceiptResponse(Guid ReceiptLineId);
