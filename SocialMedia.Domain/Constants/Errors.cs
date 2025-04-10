using FluentResults;

namespace SocialMedia.Domain.Constants;

public static class Errors
{
    public static class Post
    {
        public static readonly Error NoPostWithGivenId = new("No post with given Id");
        public static readonly Error UserIsNotAuthor = new("Current user is not an author of this post");
    }

    public static class User
    {
        public static readonly Error NoUserWithGivenUid = new("No user with given Uid");
    }
}