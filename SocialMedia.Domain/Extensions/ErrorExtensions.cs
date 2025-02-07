using FluentResults;

namespace SocialMedia.Domain.Extensions;

public static class ErrorExtensions
{
    public static Error WithResponseCode(this Error error, int code)
    {
        error.Metadata.Add("code", code);
        return error;
    }

    public static int? GetResponseCode(this IError error)
    {
        if (!error.Metadata.TryGetValue("code", out var code) || code == null) return null;

        if (code is int intCode) return intCode;

        return null;
    }
}