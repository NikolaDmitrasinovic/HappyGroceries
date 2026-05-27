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
    [HttpPost("{productId:guid}/consume")]
    [ProducesResponseType(typeof(ConsumeProductStockResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<ConsumeProductStockResponse>> ConsumeStock(
        [FromRoute]Guid productId, 
        [FromBody]ConsumeProductStockRequest request,
        CancellationToken cancellationToken)
    {
        var response = await mediator.Send(new ConsumeProductStockCommand(productId, request.Delta), cancellationToken);
        return Ok(response);
    }
    
    [HttpPost("{productId:guid}/replenish")]
    [ProducesResponseType(typeof(ReplenishProductStockResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<ReplenishProductStockResponse>> ReplenishStock(
        [FromRoute]Guid productId, 
        [FromBody]ReplenishProductStockRequest request, 
        CancellationToken cancellationToken)
    {
        var response = await mediator.Send(new ReplenishProductStockCommand(productId, request.Delta), cancellationToken);
        return Ok(response);
    }
}