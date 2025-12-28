namespace Inventory.Products.Events;

public record ProductCreated(Product Product) : IDomainEvent;
