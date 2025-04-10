using FluentResults;
using MediatR;
using SocialMedia.Application.Interfaces;
using SocialMedia.Application.Services;
using SocialMedia.Domain.Constants;

namespace SocialMedia.Application.Commands.DeletePost;

internal class DeletePostCommandHandler(IPostRepository postRepository, CurrentUserService currentUserService) : IRequestHandler<DeletePostCommand, Result>
{
    public async Task<Result> Handle(DeletePostCommand request, CancellationToken cancellationToken = default)
    {
        var post = await postRepository.GetByIdAsync(request.Id, cancellationToken);
        
        if (post == null) 
            return Result.Fail(Errors.Post.NoPostWithGivenId);
        
        if (post.AuthorId != currentUserService.User.Id) 
            return Result.Fail(Errors.Post.UserIsNotAuthor);

        await postRepository.DeleteAsync(post, cancellationToken);
        return Result.Ok();
    }
}