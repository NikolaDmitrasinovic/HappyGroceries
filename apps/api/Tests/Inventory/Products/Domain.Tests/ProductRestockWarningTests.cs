using Inventory.Products.Events;

namespace Domain.Tests;

public class ProductRestockWarningTests
{
    [Fact]
    public void SetThreshold_Raises_Warning_When_Current_Stock_is_Low_Stock()
    {
        // Arrange
        var product = ProductTestFactory.CreateProduct(stock: 0);

        // Act
        product.SetThreshold(5);
        var events = product.ClearDomainEvents();

        // Assert
        Assert.Contains(events, e => e is RestockWarningEvent);
    }
}
