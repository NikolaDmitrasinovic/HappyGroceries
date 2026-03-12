using Api.Middleware;
using Microsoft.AspNetCore.Http;
using Shared.Validation;
using System.Text;

namespace Api.Tests;

public class ExceptionHandlingMiddlewareTests
{
    [Fact]
    public async Task Middleware_WhenValidationException_ReturnsBadRequestProblemDetails()
    {
        // Arrange
        var logger = new TestLogger<ExceptionHandlingMiddleware>();

        RequestDelegate next = _ =>
            throw new RequestValidationException(
                [
                    new ValidationFailure("Name", "Name is required.")
                ]);

        var middleware = new ExceptionHandlingMiddleware(next, logger);

        var context = new DefaultHttpContext();
        context.Request.Path = "/api/products";
        context.Response.Body = new MemoryStream();

        // Act
        await middleware.InvokeAsync(context);

        // Assert
        Assert.Equal(StatusCodes.Status400BadRequest, context.Response.StatusCode);

        var body = await ReadRsponseBodyAsync(context);

        Assert.Contains("Validation failed.", body);
        Assert.Contains("errors", body);
        Assert.Contains("traceId", body);
        Assert.Contains("Name", body);
        Assert.Contains("Name is required", body);
    }

    private static async Task<string> ReadRsponseBodyAsync(DefaultHttpContext context)
    {
        context.Response.Body.Seek(0, SeekOrigin.Begin);
        using var reader = new StreamReader(context.Response.Body, Encoding.UTF8);
        return await reader.ReadToEndAsync();
    }
}
