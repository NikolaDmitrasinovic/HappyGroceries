namespace Domain.Tests;

public class ProductValidationTests
{
    [Fact]
    public static void SetStock_throws_when_negative()
    {
        // Arrange
        var product = ProductTestFactory.CreateProduct();

        // Act
        var exception = Assert.Throws<ArgumentOutOfRangeException>(() => product.SetStock(-5));

        // Assert
        Assert.Equal("stock", exception.ParamName);
    }

    [Fact]
    public static void SetThreshold_throws_when_negative()
    {
        // Arrange
        var product = ProductTestFactory.CreateProduct();

        // Act
        var exception = Assert.Throws<ArgumentOutOfRangeException>(() => product.SetThreshold(-5));

        // Assert
        Assert.Equal("threshold", exception.ParamName);
    }

    [Fact]
    public static void AdjustStock_throws_when_result_negative()
    {
        // Arrange
        var product = ProductTestFactory.CreateProduct(stock: 1);

        // Act
        var exception = Assert.Throws<ArgumentOutOfRangeException>(() => product.AdjustStock(-5));

        // Assert
        Assert.Equal("Stock", exception.ParamName);
    }
}
