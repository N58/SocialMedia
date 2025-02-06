using MediatR;
using SocialMedia.Domain.Entities;
using SocialMedia.Domain.Interfaces;

namespace SocialMedia.Application.Commands;

public record CreatePostCommand(string content) : IRequest<Guid>;

public class PlaceOrderCommandHandler(IUnitOfWork unitOfWork) : IRequestHandler<CreatePostCommand, Guid>
{
    public async Task<Guid> Handle(CreatePostCommand request, CancellationToken cancellationToken)
    {
        Post post = new()
        {
            Content = request.content,
        };
        await unitOfWork.
        await postRepository.AddAsync(post);
        return post.Id;
    }
}