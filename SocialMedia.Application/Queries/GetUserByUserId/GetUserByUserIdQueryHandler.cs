using FluentResults;
using MediatR;
using SocialMedia.Application.Interfaces;
using SocialMedia.Domain.Constants;
using SocialMedia.Domain.Entities;

namespace SocialMedia.Application.Queries.GetUserByUserId;

internal class GetUserByUserIdQueryHandler(IUserRepository userRepository)
    : IRequestHandler<GetUserByUserIdQuery, Result<User>>
{
    public async Task<Result<User>> Handle(GetUserByUserIdQuery request, CancellationToken cancellationToken)
    {
        var user = await userRepository.GetByUserIdAsync(request.Id, cancellationToken);
        if (user == null) return Result.Fail<User>(Errors.User.NoUserWithGivenUid);

        return Result.Ok(user);
    }
}