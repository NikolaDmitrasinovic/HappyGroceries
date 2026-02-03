using Carter;
using Inventory.Products.Features.CreateProduct;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Shared.Messaging;

namespace Inventory.Products.Features.AdjustProductStock;

public record AdjustProductStockRequest(Guid Id, int Delta);
public record AdjustProductStockResponse(Guid Id);

public class AdjustProductStockEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPatch("/products/stock", async (AdjustProductStockRequest requset, IMediator sender) =>
        {
            var command = new AdjustProductStockCommand(requset.Id, requset.Delta);

            var result = await sender.Send(command);

            var response = new AdjustProductStockResponse(result.Id);

            return Results.Created<AdjustProductStockResponse>($"/products/{response.Id}", response);
        })
        .WithName("AdjustProductStock")
        .Produces<CreateProductResponse>(StatusCodes.Status201Created)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .WithSummary("Adjust Product Stock")
        .WithDescription("Adjust Product Stock");
    }
}
