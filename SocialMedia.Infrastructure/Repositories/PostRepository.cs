using SocialMedia.Domain.Entities;
using SocialMedia.Domain.Interfaces;

namespace SocialMedia.Persistence.Repositories;

public class PostRepository : IPostRepository
{
    public Task AddAsync(Post entity)
    {
        throw new NotImplementedException();
    }

    public Task<Post> GetByIdAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<Post>> GetAllAsync()
    {
        throw new NotImplementedException();
    }

    public Task UpdateAsync(Post entity)
    {
        throw new NotImplementedException();
    }

    public Task DeleteAsync(Post entity)
    {
        throw new NotImplementedException();
    }
}