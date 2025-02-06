using SocialMedia.Domain.Common;

namespace SocialMedia.Domain.Entities;

public class Post : BaseEntity
{
    public required string Content { get; set; }
}