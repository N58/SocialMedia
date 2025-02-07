using FluentResults;
using Microsoft.AspNetCore.Mvc;
using SocialMedia.Domain.Extensions;

namespace SocialMedia.Application.Extensions;

public static class ResultExtensions
{
    private static readonly Dictionary<int, string> RfcUrls = new()
    {
        { 400, "https://datatracker.ietf.org/doc/html/rfc9110#section-15.5.1" }, // Bad Request
        { 401, "https://datatracker.ietf.org/doc/html/rfc9110#section-15.5.2" }, // Unauthorized
        { 403, "https://datatracker.ietf.org/doc/html/rfc9110#name-403-forbidden" }, // Forbidden
        { 404, "https://datatracker.ietf.org/doc/html/rfc9110#section-15.5.5" }, // Not Found
        { 500, "https://datatracker.ietf.org/doc/html/rfc9110#name-500-internal-server-error" } // Internal Server Error
    };

    public static ObjectResult ToResponse<T>(this Result<T> result)
    {
        var errorMessages = result.Errors.Select(e => e.Message).ToList();
        var responseCode = result.Errors.First().GetResponseCode() ?? 400;
        var problemDetails = new ProblemDetails
        {
            Type = RfcUrls[responseCode],
            Title = "One or more validation errors occurred.",
            Status = responseCode,
            Extensions =
            {
                ["errors"] = errorMessages
            }
        };

        var response = new ObjectResult(problemDetails)
        {
            StatusCode = responseCode
        };

        return response;
    }
}