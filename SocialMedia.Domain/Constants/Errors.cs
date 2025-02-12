using FluentResults;

namespace SocialMedia.Domain.Constants;

public static class Errors
{
    public static class Post
    {
        public static readonly Error NoPostWithGivenId = new("No post with given Id");
    }
}