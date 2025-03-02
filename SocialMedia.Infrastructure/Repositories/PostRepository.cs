using Microsoft.EntityFrameworkCore;
using SocialMedia.Application.Interfaces;
using SocialMedia.Domain.Entities;

namespace SocialMedia.Infrastructure.Repositories;

internal class PostRepository(AppDbContext dbContext) : BaseRepository<Post>(dbContext), IPostRepository
{
    public async Task<ICollection<Post>> GetPagedAsync(int skip, int take,
        CancellationToken cancellationToken = default)
    {
        return await dbContext.Set<Post>()
            .OrderBy(p => p.Id)
            .Skip(skip)
            .Take(take)
            .ToListAsync(cancellationToken);
    }

    public async Task<int> CountAsync(CancellationToken cancellationToken = default)
    {
        return await dbContext.Set<Post>().CountAsync(cancellationToken);
    }
}