using Receipt.Domain.Models;

namespace Receipt.Tests.TestHelpers;

public static class ReceiptTestFactory
{
    public static PurchaseReceipt CreateOpenReceipt(
        DateOnly? purchaseDate = null,
        string location = "some-location")
    {
        var date = purchaseDate ?? new DateOnly(2026, 4, 7);

        var receipt = PurchaseReceipt.Open(date, location);
        receipt.ClearDomainEvents();
        return receipt;
    }

    public static PurchaseReceipt CreateFinalizedReceipt(
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
