namespace Inventory.Products.Dtos;

public record ProductDto(
    string Name,
    List<string> Category, // TODO: consider IReadOnlyList
    string Description,
    string ImageFile,
    decimal Price,
    decimal Stock,
    decimal Threshold
    );
