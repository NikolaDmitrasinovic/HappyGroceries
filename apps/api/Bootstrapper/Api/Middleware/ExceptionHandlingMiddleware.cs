using Mapper = Api.Errors.ExceptionToProblemDetailsMapper;

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
            var problem = Mapper.Map(context, ex);

            context.Response.StatusCode = problem.Status ?? StatusCodes.Status500InternalServerError;

            if (problem.Status >= 500)
                _logger.LogError(ex, "Unhandled exception occurred.");

            context.Response.ContentType = "application/problem+json";
            await context.Response.WriteAsJsonAsync(problem);
        }
    }
}
