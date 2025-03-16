using SocialMedia.Domain.Common;

namespace SocialMedia.Domain.Entities;

public class User : BaseEntity
{
    public required string Uid { get; set; }
    public required string GivenName { get; set; }
    public required string FamilyName { get; set; }
    public required string Email { get; set; }
    public required string Image { get; set; }
}