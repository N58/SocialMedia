using FluentResults;
using MediatR;
using SocialMedia.Application.Interfaces;
using SocialMedia.Domain.Common;
using SocialMedia.Domain.Entities;

namespace SocialMedia.Application.Queries.GetPostsPaged;

internal class GetPostsPagedQueryHandler(IPostRepository postRepository)
    : IRequestHandler<GetPostsPagedQuery, Result<Paged<Post>>>
{
    public async Task<Result<Paged<Post>>> Handle(GetPostsPagedQuery request, CancellationToken cancellationToken)
    {
        var take = request.Size;
        var skip = (request.Page - 1) * request.Size;
        var posts = await postRepository.GetPagedAsync(skip, take, cancellationToken);

        var count = await postRepository.CountAsync(cancellationToken);

        var paged = new Paged<Post>(posts, count, request.Size, request.Page);

        return Result.Ok(paged);
    }
}