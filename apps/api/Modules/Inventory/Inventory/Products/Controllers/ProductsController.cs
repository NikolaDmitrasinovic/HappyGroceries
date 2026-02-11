using Microsoft.AspNetCore.Mvc;
using Shared.Messaging;

namespace Inventory.Products.Controllers;

public class ProductsController(IMediator mediator) : ControllerBase
{
}