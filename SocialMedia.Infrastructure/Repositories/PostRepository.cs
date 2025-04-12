using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using SocialMedia.Application.Dtos.Post;
using SocialMedia.Application.Extensions;
using SocialMedia.Application.Interfaces;
using SocialMedia.Domain.Common;
using SocialMedia.Domain.Entities;

namespace SocialMedia.Infrastructure.Repositories;

internal class PostRepository(AppDbContext dbContext) : BaseRepository<Post>(dbContext), IPostRepository
{
    public async Task<PostDto?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await dbContext.Set<Post>().AsNoTracking()
            .Select(p => new PostDto
            {
                Id = p.Id,
                Content = p.Content,
                AuthorGivenName = p.Author.GivenName,
                AuthorFamilyName = p.Author.FamilyName,
                AuthorImage = p.Author.Image,
                CreatedDate = p.CreatedDate,
            })
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task<Paged<PostDto>> GetPagedAsync(
        int page,
        int size,
        Expression<Func<Post, object>>? orderBy = null,
        string? sortOrder = null,
        CancellationToken cancellationToken = default)
    {
        return await dbContext.Set<Post>().AsQueryable()
            .ApplyOrdering(orderBy, sortOrder)
            .Select(p => new PostDto
            {
                Id = p.Id,
                Content = p.Content,
                AuthorGivenName = p.Author.GivenName,
                AuthorFamilyName = p.Author.FamilyName,
                AuthorImage = p.Author.Image,
                CreatedDate = p.CreatedDate
            })
            .ToPagedAsync(page, size, cancellationToken);
    }

    public async Task<int> CountAsync(CancellationToken cancellationToken = default)
    {
        return await dbContext.Set<Post>().CountAsync(cancellationToken);
    }
}