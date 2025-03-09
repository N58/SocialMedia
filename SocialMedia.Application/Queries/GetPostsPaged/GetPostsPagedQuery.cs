using FluentResults;
using MediatR;
using SocialMedia.Domain.Common;
using SocialMedia.Domain.Entities;

namespace SocialMedia.Application.Queries.GetPostsPaged;

public record GetPostsPagedQuery(int Page, int Size, string? SortColumn, string? SortOrder)
    : IRequest<Result<Paged<Post>>>;