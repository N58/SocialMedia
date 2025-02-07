using FluentResults;
using MediatR;
using SocialMedia.Domain.Entities;

namespace SocialMedia.Application.Queries.GetPost;

public record GetPostQuery(Guid Id) : IRequest<Result<Post>>;