using Asp.Versioning;
using Inventory.Products.Features.ConsumeProductStock;
using Inventory.Products.Features.ReplenishProductStock;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shared.Messaging;

namespace Inventory.Products.Controllers;

[ApiController]
[ApiVersion("2.0")]
[Route("api/v{version:apiVersion}/inventory/products")]
public class ProductsV2Controller(IMediator mediator) : ControllerBase
{
    [HttpPost("consume")]
    [ProducesResponseType(typeof(ConsumeProductStockResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<ConsumeProductStockResponse>> ConsumeStock(ConsumeProductStockRequest request, CancellationToken cancellationToken)
    {
        var response = await mediator.Send(new ConsumeProductStockCommand(request.Id, request.Delta), cancellationToken);
        return Ok(response);
    }
    
    [HttpPost("replenish")]
    [ProducesResponseType(typeof(ReplenishProductStockResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<ReplenishProductStockResponse>> ReplenishStock(ReplenishProductStockRequest request, CancellationToken cancellationToken)
    {
        var response = await mediator.Send(new ReplenishProductStockCommand(request.Id, request.Delta), cancellationToken);
        return Ok(response);
    }
}