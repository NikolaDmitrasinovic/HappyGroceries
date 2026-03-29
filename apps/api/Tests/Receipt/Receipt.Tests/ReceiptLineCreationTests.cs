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
    public void Create_Thorws_Wehn_ReceiptId_Is_Empty()
    {
        // Arrange

        // Act
        var exception = Assert.Throws<ArgumentException>(() => 
            ReceiptLine.Create(Guid.Parse("00000000-0000-0000-0000-000000000000"), "some-product", 1.5m, 1));

        // Assert
        Assert.Equal("receiptId", exception.ParamName);
    }

    [Theory]
    [InlineData("")]
    [InlineData(" ")]
    public void Create_Thorws_Wehn_ProductName_Is_Empty_or_WhiteSpace(string productName)
    {
        // Arrange

        // Act
        var exception = Assert.Throws<ArgumentException>(() =>
            ReceiptLine.Create(Guid.NewGuid(), productName, 1.5m, 1));

        // Assert
        Assert.Equal("productName", exception.ParamName);
    }
}
