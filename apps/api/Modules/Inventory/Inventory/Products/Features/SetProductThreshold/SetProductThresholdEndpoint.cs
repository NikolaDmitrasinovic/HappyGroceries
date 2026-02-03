using Carter;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Shared.Messaging;

namespace Inventory.Products.Features.SetProductThreshold;

public record SetProductThresholdRequest(Guid Id, int Threshold);
public record SetProductThresholdResponse(Guid Id);

public class SetProductThresholdEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPatch("/products/threshold", async (SetProductThresholdRequest requset, IMediator sender) =>
        {
            var command = new SetProductThresholdCommand(requset.Id, requset.Threshold);

            var result = await sender.Send(command);

            var response = new SetProductThresholdResponse(result.Id);

            return Results.Ok(response);
        })
        .WithName("SetProductThreshold")
        .Produces<SetProductThresholdResponse>(StatusCodes.Status200OK)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .WithSummary("Set Product Threshold")
        .WithDescription("Set Product Threshold");
    }
}
