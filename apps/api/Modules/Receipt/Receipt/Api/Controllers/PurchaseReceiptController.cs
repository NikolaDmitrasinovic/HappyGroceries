using Asp.Versioning;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Receipt.Api.Contracts;
using Receipt.Application.Features.AddLineToReceipt;
using Receipt.Application.Features.FinalizePurchaseReceipt;
using Receipt.Application.Features.OpenPurchaseReceipt;

namespace Receipt.Api.Controllers;

[ApiController]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/receipt/purchase-receipts")]
public class PurchaseReceiptController(IMediator mediator) : ControllerBase
{
    [HttpPost]
    [ProducesResponseType(typeof(OpenPurchaseReceiptResponse), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<OpenPurchaseReceiptResponse>> Create([FromBody] OpenPurchaseReceiptRequest request, CancellationToken cancellationToken)
    {
        var result = await mediator.Send(new OpenPurchaseReceiptCommand(request.PurchaseDate, request.Location), cancellationToken);
        var response = new OpenPurchaseReceiptResponse(result.Id);
        return Created($"{response.Id}", response);
    }

    [HttpPost("{receiptId:guid}/lines")]
    [ProducesResponseType(typeof(AddLineToReceiptResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<AddLineToReceiptResponse>> AddLine(
        [FromRoute] Guid receiptId,
        [FromBody] AddLineToReceiptRequest request,
        CancellationToken cancellationToken)
    {
        var result = await mediator.Send(new AddLineToReceiptCommand(receiptId, request.ProductName, request.UnitPrice, request.Quantity), cancellationToken);
        var response = new AddLineToReceiptResponse(result.ReceiptLineId);
        return Ok(response);
    }

    [HttpPatch("{receiptId:guid}")]
    [ProducesResponseType(typeof(FinalizePurchaseReceiptResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<FinalizePurchaseReceiptResponse>> Finalize([FromRoute] Guid receiptId, CancellationToken cancellationToken)
    {
        var result = await mediator.Send(new FinalizePurchaseReceiptCommand(receiptId), cancellationToken);
        var response = new FinalizePurchaseReceiptResponse(result.ReceiptId);
        return Ok(response);
    }
}
