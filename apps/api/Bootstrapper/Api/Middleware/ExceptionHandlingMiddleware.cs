using Api.Errors;
using Microsoft.AspNetCore.Mvc;
using Shared.Validation;

namespace Api.Middleware;

public class ExceptionHandlingMiddleware(
    RequestDelegate next,
    ILogger<ExceptionHandlingMiddleware> logger)
{
    private readonly RequestDelegate _next = next;
    private readonly ILogger<ExceptionHandlingMiddleware> _logger = logger;

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (RequestValidationException ex)
        {
            var problem = ProblemDetailsFactory.CreateValidation(
                context,
                ex.Errors);

            context.Response.StatusCode = problem.Status!.Value;

            await context.Response.WriteAsJsonAsync(problem);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Unhandled exception occurred.");

            var problem = ProblemDetailsFactory.Create(
                context,
                StatusCodes.Status500InternalServerError,
                "Server error",
                "An unexpected error occurred.");

            context.Response.StatusCode = problem.Status!.Value;

            await context.Response.WriteAsJsonAsync(problem);
        }
    }
}
