using Microsoft.AspNetCore.Mvc;

namespace Api.Errors;

public class ProblemDetailsFactory
{
    public static ProblemDetails Create(HttpContext context, int statusCode, string title, string detail)
    {
        var problemDetails = new ProblemDetails
        {
            Title = title,
            Detail = detail,
            Status = statusCode,
            Instance = context.Request.Path
        };

        problemDetails.Extensions["traceId"] = context.TraceIdentifier;

        return problemDetails;
    }

    public static ProblemDetails CreateValidation(HttpContext context, IReadOnlyCollection<object> errors)
    {
        var problemDetails = Create(
            context,
            StatusCodes.Status400BadRequest,
            "Validation failed.",
            "One or more validation errors occurred.");

        problemDetails.Extensions["errors"] = errors;

        return problemDetails;
    }
}
