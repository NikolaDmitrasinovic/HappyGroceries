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

        var body = await ReadResponseBodyAsync(context);

        Assert.Contains("Validation failed.", body);
        Assert.Contains("errors", body);
        Assert.Contains("traceId", body);
        Assert.Contains("Name", body);
        Assert.Contains("Name is required", body);
    }

    [Fact]
    public async Task Middleware_WhenUnhandledException_ReturnsInternalServerErrorProblemDetails()
    {
        // Arrange
        var logger = new TestLogger<ExceptionHandlingMiddleware>();

        RequestDelegate next = _ => throw new InvalidOperationException("boom");

        var middleware = new ExceptionHandlingMiddleware(next, logger);

        var context = new DefaultHttpContext();
        context.Request.Path = "/api/products";
        context.Response.Body = new MemoryStream();

        // Act
        await middleware.InvokeAsync(context);

        // Assert
        Assert.Equal(StatusCodes.Status500InternalServerError, context.Response.StatusCode);

        var body = await ReadResponseBodyAsync(context);

        Assert.Contains("Server error.", body);
        Assert.Contains("An unexpected error occurred.", body);
        Assert.Contains("traceId", body);
        Assert.DoesNotContain("boom", body);
    }

    [Fact]
    public async Task Middleware_WhenUnhandledExceptin_LogsError()
    {
        // Arrange
        var logger = new TestLogger<ExceptionHandlingMiddleware>();

        RequestDelegate next = _ => throw new InvalidOperationException("boom");

        var middleware = new ExceptionHandlingMiddleware(next, logger);

        var context = new DefaultHttpContext();
        context.Response.Body = new MemoryStream();

        // Act
        await middleware.InvokeAsync(context);

        // Assert
        var errorLog = Assert.Single(logger.Entries, e => e.Level == Microsoft.Extensions.Logging.LogLevel.Error);
        Assert.Equal("boom", errorLog.Exception?.Message);
    }

    private static async Task<string> ReadResponseBodyAsync(HttpContext context)
    {
        context.Response.Body.Seek(0, SeekOrigin.Begin);
        using var reader = new StreamReader(context.Response.Body, Encoding.UTF8);
        return await reader.ReadToEndAsync();
    }
}
