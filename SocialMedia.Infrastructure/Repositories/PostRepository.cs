using SocialMedia.Application.Interfaces;
using SocialMedia.Domain.Entities;

namespace SocialMedia.Infrastructure.Repositories;

internal class PostRepository(AppDbContext dbContext) : BaseRepository<Post>(dbContext), IPostRepository
{
}