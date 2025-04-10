namespace SocialMedia.API.Responses.User;

public class UserResponse
{
    public required string Id { get; set; }
    public required string GivenName { get; set; }
    public required string FamilyName { get; set; }
    public required string Email { get; set; }
    public required string Image { get; set; }
}