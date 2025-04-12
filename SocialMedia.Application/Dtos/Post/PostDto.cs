namespace SocialMedia.Application.Dtos.Post;

public class PostDto
{
    public required Guid Id { get; set; }
    public required string Content { get; set; }
    public required string AuthorGivenName { get; set; }
    public required string AuthorFamilyName { get; set; }
    public required string AuthorImage { get; set; }
    public required DateTimeOffset CreatedDate { get; set; }
}