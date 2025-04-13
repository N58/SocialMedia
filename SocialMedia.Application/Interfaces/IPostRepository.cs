using System.Linq.Expressions;
using SocialMedia.Application.Dtos.Post;
using SocialMedia.Domain.Common;
using SocialMedia.Domain.Entities;

namespace SocialMedia.Application.Interfaces;

public interface IPostRepository : IBaseRepository<Post>
{
    new Task<PostDto?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

    Task<Paged<PostDto>> GetPagedAsync(
        int page,
        int size,
        Expression<Func<Post, object>>? orderBy = null,
        string? sortOrder = null,
        CancellationToken cancellationToken = default);

    Task<int> CountAsync(CancellationToken cancellationToken = default);
}