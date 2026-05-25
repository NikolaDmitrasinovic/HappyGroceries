namespace Domain.Tests;

public class ProductStockAdjustmentTests
{
    [Fact]
    public void ConsumeStock_Decreases_Stock()
    {
        // Arrange
        var product = ProductTestFactory.CreateProduct(stock: 10);

        // Act
        product.ConsumeStock(5);

        // Assert
        Assert.Equal(5, product.Stock);
    }

    [Fact]
    public void ReplenishStock_Decreases_Stock()
    {
        // Arrange
        var product = ProductTestFactory.CreateProduct(stock: 5);

        // Act
        product.ReplenishStock(5);

        // Assert
        Assert.Equal(10, product.Stock);
    }
}