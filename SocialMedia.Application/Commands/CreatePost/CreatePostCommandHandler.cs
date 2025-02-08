using AutoMapper;
using FluentResults;
using MediatR;
using SocialMedia.Application.Interfaces;
using SocialMedia.Domain.Entities;

namespace SocialMedia.Application.Commands.CreatePost;

internal class CreatePostCommandHandler(IPostRepository postRepository, IMapper mapper)
    : IRequestHandler<CreatePostCommand, Result<Guid>>
{
    public async Task<Result<Guid>> Handle(CreatePostCommand command, CancellationToken cancellationToken)
    {
        var post = mapper.Map<Post>(command);

        await postRepository.AddAsync(post, cancellationToken);
        await postRepository.SaveChangesAsync(cancellationToken);

        return Result.Ok(post.Id);
    }
}