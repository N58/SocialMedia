using FluentResults;
using MediatR;
using SocialMedia.Application.Interfaces;

namespace SocialMedia.Application.Commands.DeletePost;

internal class DeletePostCommandHandler(IPostRepository postRepository) : IRequestHandler<DeletePostCommand, Result>
{
    public async Task<Result> Handle(DeletePostCommand request, CancellationToken cancellationToken)
    {
        var post = await postRepository.GetByIdAsync(request.Id); // TODO SET THE CANCELLATION TOKEN
        if (post == null)
        {
            return Result.Fail(""); // TODO SET THE ERROR MESSAGE
        }
        
        await postRepository.DeleteAsync(post);
        return Result.Ok();
    }
}