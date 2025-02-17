using AutoMapper;
using FluentResults;
using MediatR;
using SocialMedia.Application.Interfaces;
using SocialMedia.Domain.Constants;
using SocialMedia.Domain.Entities;

namespace SocialMedia.Application.Commands.UpdatePost;

public class UpdatePostCommandHandler(IPostRepository postRepository, IMapper mapper)
    : IRequestHandler<UpdatePostCommand, Result>
{
    public async Task<Result> Handle(UpdatePostCommand request, CancellationToken cancellationToken)
    {
        var postInDb = await postRepository.GetByIdAsync(request.Id, cancellationToken);
        if (postInDb == null) return Result.Fail(Errors.Post.NoPostWithGivenId);

        var post = mapper.Map<Post>(request);

        await postRepository.UpdateAsync(post);
        await postRepository.SaveChangesAsync(cancellationToken);
        return Result.Ok();
    }
}