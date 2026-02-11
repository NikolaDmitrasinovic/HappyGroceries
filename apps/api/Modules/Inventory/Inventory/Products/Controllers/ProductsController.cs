using Inventory.Products.Features.GetProducts;
using Microsoft.AspNetCore.Mvc;
using Shared.Messaging;

namespace Inventory.Products.Controllers;

[ApiController]
[Route("products")]
public class ProductsController(IMediator mediator) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<GetProductsResponse>> GetProducts(CancellationToken cancellationToken)
    {
        var resonse = mediator.Send(new GetProductsQuery(), cancellationToken);
        return Ok(resonse);
    }
}