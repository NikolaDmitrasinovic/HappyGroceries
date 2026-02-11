using Inventory.Products.Features.GetLowStockProducts;
using Inventory.Products.Features.GetProducts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shared.Messaging;

namespace Inventory.Products.Controllers;

[ApiController]
[Route("products")]
public class ProductsController(IMediator mediator) : ControllerBase
{
    [HttpGet]
    [ProducesResponseType(typeof(GetProductsResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<GetProductsResponse>> GetProducts(CancellationToken cancellationToken)
    {
        var resonse = await mediator.Send(new GetProductsQuery(), cancellationToken);
        return Ok(resonse);
    }

    [HttpGet("/restock")]
    [ProducesResponseType(typeof(GetLowStockProductsResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<GetLowStockProductsResponse>> GetLowStockProducts(CancellationToken cancellationToken)
    {
        var response = await mediator.Send(new GetLowStockProductsQuery(), cancellationToken);
        return Ok(response);
    }
}