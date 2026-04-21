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

    [Fact]
    public void Validate_Returns_Failure_When_PurchaseDate_Is_In_The_Future()
    {
        // Arrange
        var clock = new FakeClock(new DateTime(2026, 4, 24, 10, 0, 0, DateTimeKind.Utc));
        var validator = new OpenPurchaseReceiptValidator(clock);

        var request = new OpenPurchaseReceiptCommand(
            new DateOnly(2026, 8, 30),
            "some-location");

        // Act
        var failures = validator.Validate(request);

        // Assert
        Assert.Single(failures);
        Assert.Equal(nameof(OpenPurchaseReceiptCommand.PurchaseDate), failures.First().Property);
    }

    [Fact]
    public void Validate_Returns_Failure_When_PurchaseDate_Is_Too_Old()
    {
        // Right now too old is hard coded as older than 10 years

        // Arrange
        var clock = new FakeClock(new DateTime(2026, 4, 24, 10, 0, 0, DateTimeKind.Utc));
        var validator = new OpenPurchaseReceiptValidator(clock);

        var request = new OpenPurchaseReceiptCommand(
            new DateOnly(2000, 4, 24),
            "some-location");

        // Act
        var failures = validator.Validate(request);

        // Assert
        Assert.Single(failures);
        Assert.Equal(nameof(OpenPurchaseReceiptCommand.PurchaseDate), failures.First().Property);
    }

    [Fact]
    public void Validate_Returns_No_Failures_When_PurchaseDate_Is_Today()
    {
        // Arrange
        var clock = new FakeClock(new DateTime(2026, 4, 24, 10, 0, 0, DateTimeKind.Utc));
        var validator = new OpenPurchaseReceiptValidator(clock);

        var request = new OpenPurchaseReceiptCommand(
            new DateOnly(2026, 4, 24),
            "some-location");

        // Act
        var failures = validator.Validate(request);

        // Assert
        Assert.Empty(failures);
    }

    [Fact]
    public void Validate_Returns_No_Failures_When_PurchaseDate_Equals_MinimumDate()
    {
        // Right now minimumDate is hard coded as 10 years ago

        // Arrange
        var clock = new FakeClock(new DateTime(2026, 4, 24, 10, 0, 0, DateTimeKind.Utc));
        var validator = new OpenPurchaseReceiptValidator(clock);

        var request = new OpenPurchaseReceiptCommand(
            new DateOnly(2016, 4, 24),
            "some-location");

        // Act
        var failures = validator.Validate(request);

        // Assert
        Assert.Empty(failures);
    }
}
