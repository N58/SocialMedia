using FluentResults;
using SocialMedia.Domain.Extensions;

namespace SocialMedia.Domain.Constants;

public static class Errors
{
    public static class Post
    {
        public static readonly Error ContentIsRequired = new("Content is required");
        public static readonly Error ContentExceedsMinLength = new("Content must be at least 3 characters.");
        public static readonly Error ContentExceedsMaxLength = new("Content cannot exceed 1000 characters.");

        public static readonly Error NoPostWithGivenId = new Error("No post with given Id").WithResponseCode(404);
    }
}