using Receipt.Application.Features.OpenPurchaseReceipt;
using Receipt.Tests.TestHelpers;

namespace Receipt.Tests.Application;

public class OpenPurchaseReceiptValidatorTests
{
    [Fact]
    public void Validate_Returns_No_Failures_When_PurchaseDate_Is_Valid()
    {
        // Arrange
        var clock = new FakeClock(new DateTime(2026, 4, 24, 10, 0 , 0, DateTimeKind.Utc));
        var validator = new OpenPurchaseReceiptValidator(clock);

        var request = new OpenPurchaseReceiptCommand(
            new DateOnly(2026, 4, 20),
            "some-location");

        // Act
        var failures = validator.Validate(request);

        // Assert
        Assert.Empty(failures);
    }
}
