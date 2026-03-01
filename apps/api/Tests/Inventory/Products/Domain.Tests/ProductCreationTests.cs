using Inventory.Products.Models;

namespace Domain.Tests;

public class ProductCreationTests
{
    [Fact]
    public void Create_Throws_When_Name_Null_or_Empty()
    {
        // Arrange

        // Act
        var exception = Assert.Throws<ArgumentException>(() =>Product.Create(""));

        // Assert
        Assert.Equal("name", exception.ParamName);
    }

    [Fact]
    public void Create_Sets_Name_Stock_Threshold()
    {
        // Arrange
        var name = "NewProduct";
        var stock = 4;
        var threshold = 2;

        // Act
        var product = Product.Create(name, stock, threshold);

        // Assert
        Assert.Equal(name, product.Name);
        Assert.Equal(stock, product.Stock);
        Assert.Equal(threshold, product.Threshold);
    }
}
