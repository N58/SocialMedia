using System.Linq.Expressions;
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
        Expression<Func<Post, object>> orderBy = request.SortColumn?.ToLower() switch
        {
            "id" => p => p.Id,
            "created" => p => p.CreatedDate,
            "updated" => p => p.UpdatedDate ?? DateTimeOffset.MaxValue,
            _ => p => p.Id
        };

        var posts = await postRepository.GetPagedAsync(request.Page, request.Size, orderBy, request.SortOrder,
            cancellationToken);

        return Result.Ok(posts);
    }
}