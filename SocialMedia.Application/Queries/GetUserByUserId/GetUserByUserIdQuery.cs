using FluentResults;
using MediatR;
using SocialMedia.Domain.Entities;

namespace SocialMedia.Application.Queries.GetUserByUserId;

public record GetUserByUserIdQuery(string Id) : IRequest<Result<User>>;