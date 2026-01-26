namespace Inventory.Data.Seed;

public static class InitialData
{
    public static IEnumerable<Product> Products =>
        [
            Product.Create("Pasta", 5, 2),
            Product.Create("Sausage", 3, 1),
            Product.Create("Rice", 3, 2),
            Product.Create("Milk", 5, 1)
        ];
}
