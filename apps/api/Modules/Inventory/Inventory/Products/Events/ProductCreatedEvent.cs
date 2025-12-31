namespace Inventory.Products.Events;

public record ProductCreatedEvent(Product Product) : IDomainEvent;
