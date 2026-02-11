using Inventory.Products.Features.AdjustProductStock;
using Inventory.Products.Features.CreateProduct;
using Inventory.Products.Features.GetLowStockProducts;
using Inventory.Products.Features.GetProducts;
using Inventory.Products.Features.SetProductThreshold;
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
    public async Task<ActionResult<GetProductsResponse>> GetAll(CancellationToken cancellationToken)
    {
        var resonse = await mediator.Send(new GetProductsQuery(), cancellationToken);
        return Ok(resonse);
    }

    [HttpGet("/restock")]
    [ProducesResponseType(typeof(GetLowStockProductsResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<GetLowStockProductsResponse>> GetAllLowStock(CancellationToken cancellationToken)
    {
        var response = await mediator.Send(new GetLowStockProductsQuery(), cancellationToken);
        return Ok(response);
    }

    [HttpPost]
    [ProducesResponseType(typeof(CreateProductResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<CreateProductResponse>> Create(CreateProductRequest request, CancellationToken cancellationToken)
    {
        var response = await mediator.Send(new CreateProductCommand(request.Product), cancellationToken);
        return Created($"{response.Id}", response);
    }

    [HttpPatch]
    [ProducesResponseType(typeof(SetProductThresholdResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<SetProductThresholdResponse>> SetThreshold(SetProductThresholdRequest request, CancellationToken cancellationToken)
    {
        var response = await mediator.Send(new SetProductThresholdCommand(request.Id, request.Threshold), cancellationToken);
        return Ok(response);
    }

    [HttpPatch]
    [ProducesResponseType(typeof(AdjustProductStockResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<AdjustProductStockResponse>> AdjustStock(AdjustProductStockRequest request, CancellationToken cancellationToken)
    {
        var response = await mediator.Send(new AdjustProductStockCommand(request.Id, request.Delta), cancellationToken);
        return Ok(response);
    }
}