using FluentResults;
using Microsoft.AspNetCore.Mvc;

namespace SocialMedia.Application.Extensions;

public static class ResultExtensions
{
    public static ObjectResult ToBadRequest<T>(this Result<T> result)
    {
        var errorMessages = result.Errors.Select(e => e.Message).ToList();
        var problemDetails = new ProblemDetails
        {
            Status = 400,
            Extensions =
            {
                ["errors"] = errorMessages
            }
        };
        return new BadRequestObjectResult(problemDetails);
    }
}