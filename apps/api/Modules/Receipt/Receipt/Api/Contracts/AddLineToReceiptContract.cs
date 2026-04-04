namespace Receipt.Api.Contracts;

public record AddLineToReceiptRequest(Guid ReceiptId, string ProductName, decimal Unitprice, int Quantity);

public record AddLineToReceiptResponse(Guid ReceiptLineId);
