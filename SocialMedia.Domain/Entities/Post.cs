using SocialMedia.Domain.Common;

namespace SocialMedia.Domain.Entities;

public class CreatePostCommandHandler : BaseEntity
{
    public required string Content { get; set; }
}