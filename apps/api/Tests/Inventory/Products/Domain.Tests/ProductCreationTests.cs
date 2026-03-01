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
}
