namespace SocialMedia.API.Responses.Post;

public class PostResponse
{
    public required Guid Id { get; set; }
    public required string Content { get; set; }
    public required DateTimeOffset CreatedDate { get; set; }
}