using SocialMedia.Domain.Entities;

namespace SocialMedia.Application.Services;

public class CurrentUserService
{
    public required User User { get; set; }
}