using FluentResults;
using MediatR;
using SocialMedia.Application.Interfaces;

namespace SocialMedia.Application.Commands.DeletePost;

internal class DeletePostCommandHandler(IPostRepository postRepository) : IRequestHandler<DeletePostCommand, Result>
{
    public async Task<Result> Handle(DeletePostCommand request, CancellationToken cancellationToken = default)
    {
        var post = await postRepository.GetByIdAsync(request.Id, cancellationToken);
        if (post == null)
        {
            return Result.Fail(""); // TODO SET THE ERROR MESSAGE
        }
        
        await postRepository.DeleteAsync(post, cancellationToken);
        return Result.Ok();
    }
}