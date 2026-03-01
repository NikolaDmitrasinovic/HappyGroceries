namespace Domain.Tests;

public class ProductLowStockRulesTests
{
    [Fact]
    public void IsLowStock_True_When_Stock_Equals_Threshold()
    {
        // Arrange
        var product = ProductTestFactory.CreateProduct(stock: 5, threshold: 4);

        // Act
        product.AdjustStock(-1);

        // Assert
        Assert.True(product.IsLowStock);
    }

    [Fact]
    public void IsLowStock_True_When_Stock_Below_Threshold()
    {
        // Arrange
        var product = ProductTestFactory.CreateProduct(stock: 5, threshold: 4);

        // Act
        product.AdjustStock(-4);

        // Assert
        Assert.True(product.IsLowStock);
    }

    [Fact]
    public void IsLowStock_False_When_Stock_Above_Threshold()
    {
        // Arrange
        var product = ProductTestFactory.CreateProduct(stock: 0, threshold: 0);

        // Act
        product.AdjustStock(5);

        // Assert
        Assert.False(product.IsLowStock);
    }
}
