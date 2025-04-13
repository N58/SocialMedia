using FluentResults;
using MediatR;
using SocialMedia.Application.Dtos.Post;
using SocialMedia.Application.Interfaces;
using SocialMedia.Domain.Constants;

namespace SocialMedia.Application.Queries.GetPost;

internal class GetPostQueryHandler(IPostRepository postRepository) : IRequestHandler<GetPostQuery, Result<PostDto>>
{
    public async Task<Result<PostDto>> Handle(GetPostQuery request, CancellationToken cancellationToken)
    {
        var post = await postRepository.GetByIdAsync(request.Id, cancellationToken);
        if (post == null) return Result.Fail<PostDto>(Errors.Post.NoPostWithGivenId);

        return Result.Ok(post);
    }
}