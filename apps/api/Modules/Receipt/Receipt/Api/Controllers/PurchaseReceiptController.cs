using Asp.Versioning;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Receipt.Api.Contracts;
using Receipt.Application.Features.OpenPurchaseReceipt;
using Shared.Messaging;

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
}
