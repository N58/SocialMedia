using AutoMapper;
using FluentResults;
using MediatR;
using SocialMedia.Application.Interfaces;
using SocialMedia.Application.Services;
using SocialMedia.Domain.Constants;
using SocialMedia.Domain.Entities;

namespace SocialMedia.Application.Commands.UpdatePost;

public class UpdatePostCommandHandler(
    IPostRepository postRepository,
    IMapper mapper,
    CurrentUserService currentUserService)
    : IRequestHandler<UpdatePostCommand, Result>
{
    public async Task<Result> Handle(UpdatePostCommand request, CancellationToken cancellationToken)
    {
        var postInDb = await postRepository.GetByIdAsync(request.Id, cancellationToken);

        if (postInDb == null)
            return Result.Fail(Errors.Post.NoPostWithGivenId);

        if (postInDb.AuthorId != currentUserService.User.Id)
            return Result.Fail(Errors.Post.UserIsNotAuthor);

        var post = mapper.Map<Post>(request);

        await postRepository.UpdateAsync(post);
        await postRepository.SaveChangesAsync(cancellationToken);
        return Result.Ok();
    }
}