using Carter;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Shared.Messaging;

namespace Inventory.Products.Features.CreateProduct;

public record CreateProductRequest(ProductDto Product);
public record CreateProductResponse(Guid Id);

public class CreateProductEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("/products", async (CreateProductRequest requset, IMediator sender) =>
        {
            var command = new CreateProductCommand(new ProductDto(requset.Product.Name, requset.Product.Stock, requset.Product.Threshold));

            var result = await sender.Send(command);

            var response = new CreateProductResponse(result.Id);

            return Results.Created<CreateProductResponse>($"/products/{response.Id}", response);
        })
        .WithName("CreteProduct")
        .Produces<CreateProductResponse>(StatusCodes.Status201Created)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .WithSummary("Create Product")
        .WithDescription("Create Product");
    }
}
