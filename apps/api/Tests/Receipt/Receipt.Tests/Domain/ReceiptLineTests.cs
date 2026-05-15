using Receipt.Domain.Models;

namespace Receipt.Tests.Domain;

public class ReceiptLineTests
{
    [Fact]
    public void Create_Sets_All_Properties_Correctly()
    {
        // Arrange
        var receiptId = Guid.NewGuid();
        var productId = Guid.NewGuid();
        const string productName = "some-product";
        const decimal unitPrice = 1.5m;
        const int quantity = 1;

        // Act
        var receiptLine = ReceiptLine.Create(receiptId, productId, productName, unitPrice, quantity);

        // Assert
        Assert.Equal(receiptId, receiptLine.ReceiptId);
        Assert.Equal(productId, receiptLine.ProductId);
        Assert.Equal(productName, receiptLine.ProductName);
        Assert.Equal(unitPrice, receiptLine.UnitPrice);
        Assert.Equal(quantity, receiptLine.Quantity);
    }

    [Fact]
    public void Create_Allows_Null_ProductId()
    {
        // This is intended behavior until we wire Inventory-Receipt modules

        // Arrange
        Guid? productId = null;

        // Act
        var receiptLine = ReceiptLine.Create(Guid.NewGuid(), productId, "some-product", 1.5m, 1);

        // Assert
        Assert.Null(receiptLine.ProductId);
    }

    [Fact]
    public void Create_Throws_When_ReceiptId_Is_Empty()
    {
        // Arrange

        // Act
        var exception = Assert.Throws<ArgumentException>(() =>
            ReceiptLine.Create(Guid.Empty, Guid.NewGuid(), "some-product", 1.5m, 1));

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
            ReceiptLine.Create(Guid.NewGuid(), Guid.NewGuid(), productName, 1.5m, 1));

        // Assert
        Assert.Equal("productName", exception.ParamName);
    }

    [Fact]
    public void Create_Allows_Zero_UnitPrice()
    {
        // Arrange
        var unitPrice = 0m;

        // Act
        var receiptLine = ReceiptLine.Create(Guid.NewGuid(), Guid.NewGuid(), "some-product", unitPrice, 1);

        // Assert
        Assert.Equal(unitPrice, receiptLine.UnitPrice);
        Assert.Equal(0m, receiptLine.LineTotal);
    }

    [Fact]
    public void Create_Throws_When_UnitPrice_Is_Negative()
    {
        // Arrange
        const decimal negativePrice = -1.5m;

        // Act
        var exception = Assert.Throws<ArgumentOutOfRangeException>(() =>
            ReceiptLine.Create(Guid.NewGuid(), Guid.NewGuid(), "some-product", negativePrice, 1));

        // Assert
        Assert.Equal("unitPrice", exception.ParamName);
    }

    [Theory]
    [InlineData(-1)]
    [InlineData(0)]
    public void Create_Throws_When_Quantity_Is_Negative_or_Zero(int quantity)
    {
        // Arrange

        // Act
        var exception = Assert.Throws<ArgumentOutOfRangeException>(() =>
            ReceiptLine.Create(Guid.NewGuid(), Guid.NewGuid(), "some-product", 1.5m, quantity));

        // Assert
        Assert.Equal("quantity", exception.ParamName);
    }

    [Fact]
    public void Create_Calculates_LineTotal_Correctly()
    {
        // Arrange
        const decimal unitPrice = 1.0m;
        const int quantity = 2;

        // Act
        var receiptLine = ReceiptLine.Create(Guid.NewGuid(), Guid.NewGuid(), "some-product", unitPrice, quantity);

        // Assert
        Assert.Equal(unitPrice * quantity, receiptLine.LineTotal);
    }
}
