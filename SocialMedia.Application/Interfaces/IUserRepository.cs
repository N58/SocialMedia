using SocialMedia.Domain.Entities;

namespace SocialMedia.Application.Interfaces;

public interface IUserRepository : IBaseRepository<User>
{
    Task<User?> GetByUserIdAsync(string uid, CancellationToken cancellationToken = default);
    Task SyncUser(User user, CancellationToken cancellationToken = default);
}