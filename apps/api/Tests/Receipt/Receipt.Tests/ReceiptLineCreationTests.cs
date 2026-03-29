using Receipt.Domain.Models;

namespace Receipt.Tests;

public class ReceiptLineCreationTests
{
    [Fact]
    public void Create_Sets_All_Properties_Correctly()
    {
        // Arrange
        var guid = Guid.NewGuid();
        var productName = "some-product";
        var unitPrice = 1.5m;
        var quantity = 1;

        // Act
        var receiptLine = ReceiptLine.Create(guid, productName, unitPrice, quantity);

        // Assert
        Assert.NotNull(receiptLine);
        Assert.Equal(guid, receiptLine.ReceiptId);
        Assert.Equal(productName, receiptLine.ProductName);
        Assert.Equal(unitPrice, receiptLine.UnitPrice);
        Assert.Equal(quantity, receiptLine.Quantity);
    }

    [Fact]
    public void Create_Throws_When_ReceiptId_Is_Empty()
    {
        // Arrange

        // Act
        var exception = Assert.Throws<ArgumentException>(() => 
            ReceiptLine.Create(Guid.Empty, "some-product", 1.5m, 1));

        // Assert
        Assert.Equal("receiptId", exception.ParamName);
    }

    [Theory]
    [InlineData("")]
    [InlineData(" ")]
    public void Create_Throws_When_ProductName_Is_Empty_or_WhiteSpace(string productName)
    {
        // Arrange

        // Act
        var exception = Assert.Throws<ArgumentException>(() =>
            ReceiptLine.Create(Guid.NewGuid(), productName, 1.5m, 1));

        // Assert
        Assert.Equal("productName", exception.ParamName);
    }

    [Fact]
    public void Create_Allows_Zero_UnitPrice()
    {
        // Arrange
        var unitPrice = 0m;

        // Act
        var receiptLine = ReceiptLine.Create(Guid.NewGuid(), "some-product", unitPrice, 1);

        // Assert
        Assert.Equal(unitPrice, receiptLine.UnitPrice);
        Assert.Equal(0m, receiptLine.LineTotal);
    }

    [Fact]
    public void Create_Throws_When_UnitPrice_Is_Negative()
    {
        // Arrange
        var negativePrice = -1.5m;

        // Act
        var exception = Assert.Throws<ArgumentOutOfRangeException>(() =>
            ReceiptLine.Create(Guid.NewGuid(), "some-product", negativePrice, 1));

        // Assert
        Assert.Equal("unitPrice", exception.ParamName);
    }

    [Theory]
    [InlineData(-1)]
    [InlineData(0)]
    public void Create_Throws_When_Quntity_Is_Negative_or_Zero(int quantity)
    {
        // Arrange

        // Act
        var exception = Assert.Throws<ArgumentOutOfRangeException>(() =>
            ReceiptLine.Create(Guid.NewGuid(), "some-product", 1.5m, quantity));

        // Assert
        Assert.Equal("quantity", exception.ParamName);
    }

    [Fact]
    public void Create_Calculates_LineTotal_Correctly()
    {
        // Arrange
        var unitPrice = 1.0m;
        var quantity = 2;

        // Act
        var receiptLine = ReceiptLine.Create(Guid.NewGuid(), "some-product", unitPrice, quantity);

        // Assert
        Assert.Equal(unitPrice * quantity, receiptLine.LineTotal);
    }
}
