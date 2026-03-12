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
        catch (Exception ex)
        {
            var problem = MapException(context, ex);

            if (problem.Status >= 500)
                _logger.LogError(ex, "Unhandled exception occurred.");

            context.Response.StatusCode = problem.Status!.Value;
            context.Response.ContentType = "application/json";

            await context.Response.WriteAsJsonAsync(problem);
        }
    }

    private static ProblemDetails MapException(HttpContext context, Exception exception)
    {
        return exception switch
        {
            RequestValidationException validationException
                => ProblemDetailsFactory.CreateValidation(context, validationException.Errors),

            _ => ProblemDetailsFactory.Create(
                context,
                StatusCodes.Status500InternalServerError,
                "Server error",
                "An unexpected error occured.")
        };
    }
}
