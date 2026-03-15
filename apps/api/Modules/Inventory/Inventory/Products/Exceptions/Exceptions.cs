using Shared.Exceptions;

namespace Inventory.Products.Exceptions;

public class ProductNotFoundException(Guid Id) : NotFoundException($"Product with id '{Id}' was not found.");