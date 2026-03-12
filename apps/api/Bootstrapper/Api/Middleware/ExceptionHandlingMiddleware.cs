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
            await HandleValidationExceptionAsync(context, ex);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Unhandled exception occurred.");

            await HandleInternalServerErrorAsync(context);
        }
    }

    private async Task HandleValidationExceptionAsync(HttpContext context, RequestValidationException ex)
    {
        throw new NotImplementedException();
    }

    private async Task HandleInternalServerErrorAsync(HttpContext context)
    {
        throw new NotImplementedException();
    }
}
