namespace Inventory.Products.Dtos;

public record ProductDto(
    string Name,
    List<string> Category,
    string Description,
    string ImageFile,
    decimal Price,
    decimal Stock,
    decimal Threshold
    );
