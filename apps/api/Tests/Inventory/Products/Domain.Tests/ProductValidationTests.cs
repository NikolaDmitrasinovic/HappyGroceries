namespace Domain.Tests;

public class ProductValidationTests
{
    [Fact]
    public void SetStock_Throws_When_Negative()
    {
        // Arrange
        var product = ProductTestFactory.CreateProduct();

        // Act
        var exception = Assert.Throws<ArgumentOutOfRangeException>(() => product.SetStock(-5));

        // Assert
        Assert.Equal("stock", exception.ParamName);
    }

    [Fact]
    public void SetThreshold_Throws_When_Negative()
    {
        // Arrange
        var product = ProductTestFactory.CreateProduct();

        // Act
        var exception = Assert.Throws<ArgumentOutOfRangeException>(() => product.SetThreshold(-5));

        // Assert
        Assert.Equal("threshold", exception.ParamName);
    }

    [Fact]
    public void AdjustStock_Throws_When_Result_Negative()
    {
        // Arrange
        var product = ProductTestFactory.CreateProduct(stock: 1);

        // Act
        var exception = Assert.Throws<ArgumentOutOfRangeException>(() => product.AdjustStock(-5));

        // Assert
        Assert.Equal("delta", exception.ParamName);
    }
    
    [Fact]
    public void ConsumeStock_Throws_When_Delta_Is_Negative()
    {
        // Arrange
        var product = ProductTestFactory.CreateProduct();

        // Act
        var exception = Assert.Throws<ArgumentOutOfRangeException>(() => product.ConsumeStock(-5));

        // Assert
        Assert.Equal("delta", exception.ParamName);
    }
    
    [Fact]
    public void ConsumeStock_Throws_When_Result_Negative()
    {
        // Arrange
        var product = ProductTestFactory.CreateProduct(stock: 1);

        // Act
        var exception = Assert.Throws<ArgumentOutOfRangeException>(() => product.ConsumeStock(-5));

        // Assert
        Assert.Equal("delta", exception.ParamName);
    }
    
    [Fact]
    public void ReplenishStock_Throws_When_Delta_Is_Negative()
    {
        // Arrange
        var product = ProductTestFactory.CreateProduct();

        // Act
        var exception = Assert.Throws<ArgumentOutOfRangeException>(() => product.ReplenishStock(-5));

        // Assert
        Assert.Equal("delta", exception.ParamName);
    }
}
