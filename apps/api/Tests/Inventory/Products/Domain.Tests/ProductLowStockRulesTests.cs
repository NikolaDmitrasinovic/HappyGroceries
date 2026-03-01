namespace Domain.Tests;

public class ProductLowStockRulesTests
{
    [Fact]
    public void IsLowStock_True_When_Stock_Equals_Threshold()
    {
        // Arrange
        var product = ProductTestFactory.CreateProduct(stock: 4, threshold: 4);

        // Act

        // Assert
        Assert.True(product.IsLowStock);
    }

    [Fact]
    public void IsLowStock_True_When_Stock_Below_Threshold()
    {
        // Arrange
        var product = ProductTestFactory.CreateProduct(stock: 1, threshold: 4);

        // Act

        // Assert
        Assert.True(product.IsLowStock);
    }

    [Fact]
    public void IsLowStock_False_When_Stock_Above_Threshold()
    {
        // Arrange
        var product = ProductTestFactory.CreateProduct(stock: 4, threshold: 0);

        // Act

        // Assert
        Assert.False(product.IsLowStock);
    }
}
