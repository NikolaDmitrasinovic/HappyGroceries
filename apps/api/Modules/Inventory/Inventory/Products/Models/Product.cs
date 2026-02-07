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

        var product = new Product
        {
            Id = Guid.NewGuid(),
            Name = name,
        };

        product.SetStock(stock);
        product.SetThreshold(threshold);

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
    }

    public void SetThreshold(int threshold)
    {
        ArgumentOutOfRangeException.ThrowIfNegative(threshold);

        Threshold = threshold;
    }

    public void AdjustStock(int delta)
    {
        ArgumentOutOfRangeException.ThrowIfNegative(Stock + delta);

        Stock += delta;

        if (IsLowStock)
            this.AddDomainEvent(new RestockWarningEvent(this));
    }
}
