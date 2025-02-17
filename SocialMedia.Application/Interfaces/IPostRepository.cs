using SocialMedia.Domain.Entities;

namespace SocialMedia.Application.Interfaces;

public interface IPostRepository : IBaseRepository<Post>
{
    Task<ICollection<Post>> GetPagedAsync(int skip, int take, CancellationToken cancellationToken = default);

    Task<int> CountAsync(CancellationToken cancellationToken = default);
}