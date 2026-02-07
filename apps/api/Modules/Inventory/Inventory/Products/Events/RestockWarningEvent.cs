namespace Inventory.Products.Events;

public record RestockWarningEvent(Product Product) : IDomainEvent;
