using Carter;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Shared.Messaging;

namespace Inventory.Products.Features.GetLowStockProducts;

public record GetLowStockProductsResponse(IEnumerable<ProductDto> Products);

public class GetLowStockProductsEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/products/restock", async (IMediator sender) =>
        {
            var result = await sender.Send(new GetLowStockProductsQuery());

            var response = new List<ProductDto>();
            foreach (var product in result.Products)
            {
                var productDto = new ProductDto(product.Name, product.Stock, product.Threshold);
                response.Add(productDto);
            }

            return Results.Ok(response);
        })
        .WithName("GetLowStockProducts")
        .Produces<GetLowStockProductsResponse>(StatusCodes.Status200OK)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .WithSummary("Get Low-Stock Products")
        .WithDescription("Get Low-Stock Products");
    }
}
