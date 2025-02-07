namespace SocialMedia.API.Responses.Post;

public class PostResponse
{
    public Guid Id { get; set; }
    public required string Content { get; set; }
    public DateTime CreatedDate { get; set; }
}