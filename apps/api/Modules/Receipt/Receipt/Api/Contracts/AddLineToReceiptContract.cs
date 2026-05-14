namespace Receipt.Api.Contracts;

public record AddLineToReceiptRequest(Guid? ProductId, string ProductName, decimal UnitPrice, int Quantity);

public record AddLineToReceiptResponse(Guid ReceiptLineId);
