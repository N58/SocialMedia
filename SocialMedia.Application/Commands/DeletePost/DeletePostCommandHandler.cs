using FluentResults;
using MediatR;
using SocialMedia.Application.Interfaces;
using SocialMedia.Domain.Constants;

namespace SocialMedia.Application.Commands.DeletePost;

internal class DeletePostCommandHandler(IPostRepository postRepository)
    : IRequestHandler<DeletePostCommand, Result>
{
    public async Task<Result> Handle(DeletePostCommand request, CancellationToken cancellationToken = default)
    {
        var post = await postRepository.GetEntityByIdAsync(request.Id, cancellationToken);

        if (post == null)
            return Result.Fail(Errors.Post.NoPostWithGivenId);

        if (post.AuthorId != request.AuthorId)
            return Result.Fail(Errors.Post.UserIsNotAuthor);

        await postRepository.DeleteAsync(post, cancellationToken);
        return Result.Ok();
    }
}