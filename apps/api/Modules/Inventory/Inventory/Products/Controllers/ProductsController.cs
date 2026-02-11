using Inventory.Products.Features.GetLowStockProducts;
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
        var resonse = await mediator.Send(new GetProductsQuery(), cancellationToken);
        return Ok(resonse);
    }

    [HttpGet("/restock")]
    public async Task<ActionResult<GetLowStockProductsResponse>> GetLowStockProducts(CancellationToken cancellationToken)
    {
        var response = await mediator.Send(new GetLowStockProductsQuery(), cancellationToken);
        return Ok(response);
    }
}