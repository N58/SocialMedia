using FluentResults;
using MediatR;
using SocialMedia.Domain.Common;
using SocialMedia.Domain.Entities;

namespace SocialMedia.Application.Queries.GetPostsPaged;

public record GetPostsPagedQuery(int Page, int Size, string? SortColumn = null, string? SortOrder = null)
    : IRequest<Result<Paged<Post>>>;