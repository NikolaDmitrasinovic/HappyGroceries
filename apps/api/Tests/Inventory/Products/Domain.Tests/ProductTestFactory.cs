using Inventory.Products.Models;

namespace Domain.Tests;

public static class ProductTestFactory
{
    public static Product CreateProduct(
        string name = "NewProduct",
        int stock = 0,
        int threshold = 0)
    {
        var product = Product.Create(name, stock, threshold);
        product.ClearDomainEvents();
        return product;
    }
}
