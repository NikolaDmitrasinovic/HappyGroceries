namespace Domain.Tests;

public class ProductValidationTests
{
    [Fact]
    public static void SetStock_throws_when_negative()
    {
        // Arrange
        var product = ProductTestFactory.CreateProductHelper();

        // Act

        // Assert
        Assert.Throws<ArgumentOutOfRangeException>(() => product.SetStock(-5));
    }
}
