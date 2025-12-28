namespace Inventory.Products.Events;

public record ProductPriceChanged(Product Product) : IDomainEvent;
