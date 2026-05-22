using Inventory.Products.Events;

namespace Inventory.Products.Models;

public class Product : Aggregate<Guid>
{
    public string Name { get; private set; } = default!;
    public int Stock { get; private set; }
    public int Threshold { get; private set; }

    public bool IsLowStock => Stock <= Threshold;

    public static Product Create(string name, int stock = 0, int threshold = 0)
    {
        ArgumentException.ThrowIfNullOrEmpty(name);
        ArgumentOutOfRangeException.ThrowIfNegative(stock, nameof(stock));
        ArgumentOutOfRangeException.ThrowIfNegative(threshold, nameof(threshold));

        var product = new Product
        {
            Id = Guid.NewGuid(),
            Name = name,
            Stock = stock,
            Threshold = threshold
        };

        product.AddDomainEvent(new ProductCreatedEvent(product));

        return product;
    }

    public void Update(string name)
    {
        ArgumentException.ThrowIfNullOrEmpty(name);

        Name = name;
    }

    public void SetStock(int stock)
    {
        ArgumentOutOfRangeException.ThrowIfNegative(stock, nameof(stock));

        Stock = stock;

        if (IsLowStock)
            AddDomainEvent(new RestockWarningEvent(this));
    }

    public void SetThreshold(int threshold)
    {
        ArgumentOutOfRangeException.ThrowIfNegative(threshold, nameof(threshold));

        Threshold = threshold;

        if (IsLowStock)
            AddDomainEvent(new RestockWarningEvent(this));
    }

    public void ReplenishStock(int delta)
    {
        ArgumentOutOfRangeException.ThrowIfNegative(delta, nameof(delta));
        AdjustStock(delta);
    }

    public void ConsumeStock(int delta)
    {
        ArgumentOutOfRangeException.ThrowIfNegative(delta, nameof(delta));

        AdjustStock(-delta);
    }

    public void AdjustStock(int delta)
    {
        var wasLowStock = IsLowStock;

        ArgumentOutOfRangeException.ThrowIfNegative(Stock + delta, nameof(delta));

        Stock += delta;

        var isLowStock = IsLowStock;

        if (!wasLowStock && isLowStock)
            AddDomainEvent(new RestockWarningEvent(this));
    }
}
