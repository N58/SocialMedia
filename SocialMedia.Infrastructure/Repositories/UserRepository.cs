using Microsoft.EntityFrameworkCore;
using SocialMedia.Application.Interfaces;
using SocialMedia.Domain.Entities;

namespace SocialMedia.Infrastructure.Repositories;

internal class UserRepository(AppDbContext dbContext) : BaseRepository<User>(dbContext), IUserRepository
{
    public async Task<User?> GetByUserIdAsync(string uid, CancellationToken cancellationToken = default)
    {
        return await dbContext.Set<User>().AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == uid, cancellationToken);
    }

    public async Task SyncUser(User user, CancellationToken cancellationToken = default)
    {
        var existingUser = await GetByUserIdAsync(user.Id, cancellationToken);
        if (existingUser is null) await AddAsync(user, cancellationToken);
    }
}