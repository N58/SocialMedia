using AutoMapper;
using FluentResults;
using MediatR;
using SocialMedia.Application.Interfaces;
using SocialMedia.Domain.Entities;

namespace SocialMedia.Application.Commands.SyncUser;

public class SyncUserCommandHandler(IUserRepository userRepository, IMapper mapper)
    : IRequestHandler<SyncUserCommand, Result<Unit>>
{
    public async Task<Result<Unit>> Handle(SyncUserCommand request, CancellationToken cancellationToken)
    {
        var user = mapper.Map<User>(request);
        await userRepository.SyncUser(user, cancellationToken);
        await userRepository.SaveChangesAsync(cancellationToken);

        return Result.Ok(Unit.Value);
    }
}