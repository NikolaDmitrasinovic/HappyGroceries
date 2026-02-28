namespace Domain.Tests;

public class ProductValidationTests
{
    [Fact]
    public static void SetStock_throws_when_negative()
    {
        // Arrange
        var product = ProductTestFactory.CreateProductHelper();

        // Act
        var result = Assert.Throws<ArgumentOutOfRangeException>(() => product.SetStock(-5));

        // Assert
        Assert.Equal("stock ('-5') must be a non-negative value. (Parameter 'stock')\r\nActual value was -5.", result.Message);
    }

    [Fact]
    public static void SetThreshold_throws_when_negative()
    {
        // Arrange
        var product = ProductTestFactory.CreateProductHelper();

        // Act
        var result = Assert.Throws<ArgumentOutOfRangeException>(() => product.SetThreshold(-5));

        // Assert
        Assert.Equal("threshold ('-5') must be a non-negative value. (Parameter 'threshold')\r\nActual value was -5.", result.Message);
    }
}
