namespace Inventory.Data.Seed;

public static class InitialData
{
    public static IEnumerable<Product> Products =>
        [
            Product.Create(new Guid("382c74c3-721d-4f34-80e5-57657b6cbc27"), "Pasta", ["category1"], 125, "some description", "image1", 5, 2),
            Product.Create(new Guid("382c74c3-721d-4f34-80e5-57657b6cbc28"), "Sausage", ["category2"], 350, "some description", "image2", 3, 1)
        ];
}
