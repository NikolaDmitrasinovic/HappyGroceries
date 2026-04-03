namespace Receipt.Application.Dtos;

public record PurchaseReceiptDto(
    DateOnly PurchaseDate,
    string? Location);
