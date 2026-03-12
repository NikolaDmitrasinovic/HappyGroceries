using Microsoft.AspNetCore.Mvc;
using Shared.Validation;

namespace Api.Errors;

public class ExceptionToProblemDetailsMapper
{
    public static ProblemDetails Map(HttpContext context, Exception exception)
    {
        return exception switch
        {
            RequestValidationException validationException
                => ProblemDetailsFactory.CreateValidation(context, validationException.Errors),

            _ => ProblemDetailsFactory.Create(
                context,
                StatusCodes.Status500InternalServerError,
                "Server error.",
                "An unexpected error occurred.")
        };
    }
}
