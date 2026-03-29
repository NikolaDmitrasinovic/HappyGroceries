using Receipt.Domain.Models;
using ReceiptModel = Receipt.Domain.Models.Receipt;

namespace Receipt.Tests;

public class ReceiptCreationTests
{
    [Fact]
    public void Open_Sets_All_Properties_Correctly()
    {
        // Arrange
        var purchaseDate = DateOnly.Parse("2026-04-01");
        var location = "some-location";


        // Act
        var receipt = ReceiptModel.Open(purchaseDate, location);

        // Assert
        Assert.Equal(purchaseDate, receipt.PurchaseDate);
        Assert.Equal(ReceiptStatus.Open, receipt.Status);
        Assert.Equal([], receipt.Lines);
        Assert.Equal(0, receipt.TotalAmount);
        Assert.Equal(location, receipt.Location);
    }

    [Theory]
    [InlineData("")]
    [InlineData(" ")]
    public void Open_Sets_Location_Correctly_When_Not_Provided(string location)
    {
        // Arrange

        // Act
        var receipt = ReceiptModel.Open(DateOnly.MinValue, location);

        // Assert
        Assert.Equal("N/A", receipt.Location);
    }

    [Fact]
    public void AddLine_Adds_Line_And_Recalculates()
    {
        // Arrange
        var receipt = ReceiptTestFactory.CreateReceipt();

        // Act
        receipt.AddLine("some-product", 1.0m, 1);

        // Assert
        Assert.Single(receipt.Lines);
        Assert.Equal(1.0m, receipt.TotalAmount);
    }

    [Fact]
    public void AddLine_Throws_When_Finalized()
    {
        // Arrange
        var receipt = ReceiptTestFactory.CreateReceipt();
        receipt.AddLine("some-product", 1.0m, 1);
        receipt.MarkAsFinalized();

        // Act
        var exception = Assert.Throws<InvalidOperationException>(() => 
            receipt.AddLine("some-product", 1.0m, 1));

        // Assert
        Assert.Equal("Lines can only be added to an open receipt.", exception.Message);
    }

    [Fact]
    public void MarkAsFinalized_Sets_Status_As_Finalized()
    {
        // Arrange
        var receipt = ReceiptTestFactory.CreateReceipt();
        receipt.AddLine("some-product", 1.0m, 1);

        // Act
        receipt.MarkAsFinalized();

        // Assert
        Assert.Equal(ReceiptStatus.Finalized, receipt.Status);
    }

    [Fact]
    public void MarkAsFinalized_Throws_When_No_Lines()
    {
        // Arrange
        var receipt = ReceiptTestFactory.CreateReceipt();

        // Act
        var exception = Assert.Throws<InvalidOperationException>(() =>
            receipt.MarkAsFinalized());

        // Assert
        Assert.Equal("A receipt cannot be finalized without at least one line.", exception.Message);
    }

    [Fact]
    public void MarkAsFinalized_Is_Idempotent()
    {
        // Arrange
        var receipt = ReceiptTestFactory.CreateReceipt();
        receipt.AddLine("some-product", 1.0m, 1);
        receipt.MarkAsFinalized();

        // Act
        receipt.MarkAsFinalized();

        // Assert
        Assert.Equal(ReceiptStatus.Finalized, receipt.Status);
    }
}
