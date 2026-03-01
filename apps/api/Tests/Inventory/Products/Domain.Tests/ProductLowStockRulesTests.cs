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
}
