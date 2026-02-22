using System.Linq.Expressions;

namespace Inventory.Products.Queries;

public class ProductPredicates
{
    public static readonly Expression<Func<Product, bool>> IsLowStockExpression =
        p => p.Stock <= p.Threshold;
}
