namespace Receipt.Application.Features.AddLineToReceipt;

public record AddLineToReceiptCommand(Guid ReceiptId, string ProductName, decimal UnitPrice, int Quantity) : IRequest<AddLineToReceiptResult>;

public record AddLineToReceiptResult(Guid ReceiptLineId);
