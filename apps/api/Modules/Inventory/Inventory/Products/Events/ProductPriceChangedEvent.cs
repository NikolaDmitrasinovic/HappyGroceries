namespace Inventory.Products.Events;

public record ProductPriceChangedEvent(Product Product) : IDomainEvent;
