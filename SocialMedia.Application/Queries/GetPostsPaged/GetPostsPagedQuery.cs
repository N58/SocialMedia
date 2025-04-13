using FluentResults;
using MediatR;
using SocialMedia.Application.Dtos.Post;
using SocialMedia.Domain.Common;

namespace SocialMedia.Application.Queries.GetPostsPaged;

public record GetPostsPagedQuery(int Page, int Size, string? SortColumn = null, string? SortOrder = null)
    : IRequest<Result<Paged<PostDto>>>;