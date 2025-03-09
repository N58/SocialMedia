using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using SocialMedia.Application.Extensions;
using SocialMedia.Application.Interfaces;
using SocialMedia.Domain.Common;
using SocialMedia.Domain.Entities;

namespace SocialMedia.Infrastructure.Repositories;

internal class PostRepository(AppDbContext dbContext) : BaseRepository<Post>(dbContext), IPostRepository
{
    public async Task<Paged<Post>> GetPagedAsync(
        int page,
        int size,
        Expression<Func<Post, object>>? orderBy = null,
        string? sortOrder = null,
        CancellationToken cancellationToken = default)
    {
        return await dbContext.Set<Post>().AsQueryable()
            .ApplyOrdering(orderBy, sortOrder)
            .ToPagedAsync(page, size, cancellationToken);
    }

    public async Task<int> CountAsync(CancellationToken cancellationToken = default)
    {
        return await dbContext.Set<Post>().CountAsync(cancellationToken);
    }
}