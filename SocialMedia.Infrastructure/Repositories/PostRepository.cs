using SocialMedia.Application.Common;
using SocialMedia.Application.Interfaces;
using SocialMedia.Domain.Entities;

namespace SocialMedia.Infrastructure.Repositories;

public class PostRepository(AppDbContext dbContext) : BaseRepository<Post>(dbContext), IPostRepository
{
}