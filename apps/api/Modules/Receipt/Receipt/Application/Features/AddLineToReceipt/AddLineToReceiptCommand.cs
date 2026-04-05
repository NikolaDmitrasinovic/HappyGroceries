namespace Receipt.Application.Features.AddLineToReceipt;

public record AddLineToReceiptCommand(Guid ReceiptId, string ProductName, decimal UnitPrice, int Quantity) : ICommand<AddLineToReceiptResult>;

public record AddLineToReceiptResult(Guid ReceiptLineId);
