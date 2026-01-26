namespace Inventory.Products.Dtos;

public record ProductDto(
    string Name,
    int Stock,
    int Threshold
    );
