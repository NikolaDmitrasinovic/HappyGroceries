using ReceiptModel = Receipt.Domain.Models.Receipt;
using Receipt.Domain.Models;

namespace Receipt.Domain.Tests;

public static class ReceiptTestFactory
{
    public static Receipt CreateOpenReceipt(
        DateOnly? purchaseDate = null,
        string location = "some-location")
    {
        var date = purchaseDate ?? new DateOnly(2026, 4, 7);

        var receipt = Domain.Models.Receipt.Open(date, location);
        receipt.ClearDomainEvents();
        return receipt;
    }

    public static ReceiptModel CreateFinalizedReceipt(
    DateOnly? purchaseDate = null,
    string location = "some-location")
    {
        var receipt = CreateOpenReceipt(purchaseDate, location);
        receipt.AddLine("some-product", 1.0m, 1);
        receipt.MarkAsFinalized();
        receipt.ClearDomainEvents();
        return receipt;
    }
}
