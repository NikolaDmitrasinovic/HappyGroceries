using Inventory.Products.Events;
using Inventory.Products.Models;

namespace Domain.Tests;

public class ProductCreationTests
{
    [Fact]
    public void Create_Throws_When_Name_Empty()
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

    [Fact]
    public void Create_Sets_Id_to_Non_Empty_Guid()
    {
        // Arrange

        // Act
        var product = Product.Create("NewProduct");

        // Assert
        Assert.NotEqual(Guid.Empty, product.Id);
    }

    [Fact]
    public void Create_Raises_ProductCreatedEvent()
    {
        // Arrange
        var product = Product.Create("NewProduct");

        // Act
        var events = product.ClearDomainEvents();

        // Assert
        Assert.IsType<ProductCreatedEvent>(events[0]);
    }
}
