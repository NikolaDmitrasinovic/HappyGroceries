namespace Inventory.Data.Seed;

public static class InitialData
{
    public static IEnumerable<Product> Products =>
        [
            Product.Create(new Guid("382c74c3-721d-4f34-80e5-57657b6cbc27"), "Pasta", [], 125, "", "", 5, 2),
            Product.Create(new Guid("382c74c3-721d-4f34-80e5-57657b6cbc28"), "Sausage", [], 350, "", "", 3, 1)
        ];
}
