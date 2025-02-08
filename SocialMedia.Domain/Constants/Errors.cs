using FluentResults;
using SocialMedia.Domain.Extensions;

namespace SocialMedia.Domain.Constants;

public static class Errors
{
    public static class Post
    {
        public static readonly Error NoPostWithGivenId = new Error("No post with given Id").WithResponseCode(404);
    }
}