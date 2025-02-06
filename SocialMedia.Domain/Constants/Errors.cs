namespace SocialMedia.Domain.Constants;

public static class Errors
{
    public static class Post
    {
        public const string ContentIsRequired = "Content is required";
        public const string ContentExceedsMinLength = "Content must be at least 3 characters.";
        public const string ContentExceedsMaxLength = "Content cannot exceed 1000 characters.";
    }
}