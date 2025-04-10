using System.Linq.Expressions;
using Moq;
using Shouldly;
using SocialMedia.Application.Interfaces;
using SocialMedia.Application.Queries.GetPostsPaged;
using SocialMedia.Domain.Common;
using SocialMedia.Domain.Entities;

namespace SocialMedia.UnitTests.Queries.GetPostsPaged;

public class GetPostsPagedQueryHandlerTests
{
    private static readonly List<Post> PostsListMock =
    [
        new()
        {
            Id = Guid.NewGuid(),
            Content = "post data 1",
            CreatedDate = DateTimeOffset.Now,
            UpdatedDate = null
        },
        new()
        {
            Id = Guid.NewGuid(),
            Content = "post data 2",
            CreatedDate = DateTimeOffset.Now - TimeSpan.FromHours(1),
            UpdatedDate = null
        },
        new()
        {
            Id = Guid.NewGuid(),
            Content = "post data 3",
            CreatedDate = DateTimeOffset.Now - TimeSpan.FromHours(2),
            UpdatedDate = null
        }
    ];

    private readonly Mock<IPostRepository> _postRepositoryMock = new();

    // private int GetTotalPages(int size)
    // {
    //     return (int)Math.Ceiling(_postListMock.Count / (decimal)size);
    // }

    [Theory]
    [InlineData(1, 5, "id", "asc")]
    [InlineData(2, 5, null, "desc")]
    [InlineData(3, 3, "id", null)]
    [InlineData(3, 3, "id", "")]
    [InlineData(3, 3, "", "desc")]
    [InlineData(2, 5, "created", "asc")]
    [InlineData(2, 5, "updated", "desc")]
    public async Task Handle_ExistingPostsPaged_ReturnsSuccess(int page, int size, string? orderBy = null,
        string? sortOrder = null)
    {
        Expression<Func<Post, object>> orderByPredicate = orderBy?.ToLower() switch
        {
            "id" => p => p.Id,
            "created" => p => p.CreatedDate,
            "updated" => p => p.UpdatedDate ?? DateTimeOffset.MaxValue,
            _ => p => p.Id
        };

        var list = PostsListMock.OrderBy(orderByPredicate.Compile()).ToList();
        var correctResult = new Paged<Post>(list, list.Count, size, page);
        var query = new GetPostsPagedQuery(page, size, orderBy, sortOrder);
        var handler = new GetPostsPagedQueryHandler(_postRepositoryMock.Object);
        _postRepositoryMock
            .Setup(x =>
                x.GetPagedAsync(page, size, It.IsAny<Expression<Func<Post, object>>>(), sortOrder,
                    It.IsAny<CancellationToken>()))
            .ReturnsAsync(correctResult);

        var result = await handler.Handle(query, CancellationToken.None);

        _postRepositoryMock.Verify(x =>
            x.GetPagedAsync(page, size, It.IsAny<Expression<Func<Post, object>>>(), sortOrder,
                It.IsAny<CancellationToken>()), Times.Once);
        result.IsSuccess.ShouldBeTrue();
        result.Value.ShouldBeEquivalentTo(correctResult);
        // result.Value.TotalCount.ShouldBe(list.Count);
        // result.Value.Size.ShouldBe(size);
        // result.Value.PageNumber.ShouldBe(page);
        // result.Value.PageNumber.ShouldBeGreaterThanOrEqualTo(1);
        // result.Value.PageNumber.ShouldBeLessThanOrEqualTo(result.Value.TotalPages);
        // result.Value.TotalPages.ShouldBe(GetTotalPages(size));
        // result.Value.TotalPages.ShouldBeGreaterThanOrEqualTo(1);
        // result.Value.Data.ShouldBeEquivalentTo(correctResult);
    }
}