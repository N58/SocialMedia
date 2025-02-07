using FluentResults;
using MediatR;
using SocialMedia.Application.Interfaces;
using SocialMedia.Domain.Constants;
using SocialMedia.Domain.Entities;

namespace SocialMedia.Application.Queries.GetPost;

internal class GetPostQueryHandler(IPostRepository postRepository) : IRequestHandler<GetPostQuery, Result<Post>>
{
    public async Task<Result<Post>> Handle(GetPostQuery request, CancellationToken cancellationToken)
    {
        var post = await postRepository.GetByIdAsync(request.Id);

        if (post == null) return Result.Fail<Post>(Errors.Post.NoPostWithGivenId);

        return Result.Ok(post)!;
    }
}