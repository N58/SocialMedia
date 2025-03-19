using SocialMedia.Domain.Common;

namespace SocialMedia.Domain.Entities;

public sealed class Post : BaseEntity
{
    public required string Content { get; set; }
    public required string AuthorId { get; set; }
    public required User Author { get; set; }
}