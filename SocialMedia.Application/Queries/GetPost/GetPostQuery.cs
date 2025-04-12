using FluentResults;
using MediatR;
using SocialMedia.Application.Dtos.Post;

namespace SocialMedia.Application.Queries.GetPost;

public record GetPostQuery(Guid Id) : IRequest<Result<PostDto>>;