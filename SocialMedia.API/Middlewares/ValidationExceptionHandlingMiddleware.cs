using FluentValidation;
using Microsoft.AspNetCore.Mvc;

namespace SocialMedia.API.Middlewares;

public sealed class ValidationExceptionHandlingMiddleware(RequestDelegate next)
{
    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await next(context);
        }
        catch (ValidationException exception)
        {
            var problemDetails = new ProblemDetails
            {
                Status = StatusCodes.Status400BadRequest,
                Type = "https://datatracker.ietf.org/doc/html/rfc9110#section-15.5.1",
                Title = "One or more validation errors occurred."
            };

            if (exception.Errors is not null)
                problemDetails.Extensions["errors"] = exception.Errors.Select(e => e.ErrorMessage);

            await context.Response.WriteAsJsonAsync(problemDetails);
        }
    }
}