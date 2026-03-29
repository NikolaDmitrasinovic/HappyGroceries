namespace Receipt.Tests;

public static class ReceiptTestFactory
{
    public static Domain.Models.Receipt CreateReceipt(
        string purchaseDate = "2026-04-07",
        string location = "some-location")
    {
        var date = DateOnly.Parse(purchaseDate);

        var receipt = Domain.Models.Receipt.Open(date, location);
        receipt.ClearDomainEvents();
        return receipt;
    }
}
