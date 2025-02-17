using Moq;
using Shouldly;
using SocialMedia.Application.Interfaces;
using SocialMedia.Application.Queries.GetPostsPaged;
using SocialMedia.Domain.Entities;

namespace SocialMedia.UnitTests.Queries.GetPostsPaged;

public class GetPostsPagedQueryHandlerTests
{
    private readonly List<Post> _postListMock = [];
    private readonly Mock<IPostRepository> _postRepositoryMock = new();

    public GetPostsPagedQueryHandlerTests()
    {
        for (var i = 0; i < 8; i++)
            _postListMock.Add(new Post
                {
                    Id = Guid.NewGuid(),
                    Content = $"post data {i}",
                    CreatedDate = default,
                    UpdatedDate = null
                }
            );
    }

    private int GetTotalPages(int size)
    {
        return (int)Math.Ceiling(_postListMock.Count / (decimal)size);
    }

    [Theory]
    [InlineData(1, 5)]
    [InlineData(2, 5)]
    [InlineData(3, 3)]
    public async Task Handle_ExistingPostsPaged_ReturnsSuccess(int page, int size)
    {
        var skip = (page - 1) * size;
        var correctResult = _postListMock.Skip(skip).Take(size).ToList();
        var query = new GetPostsPagedQuery(page, size);
        var handler = new GetPostsPagedQueryHandler(_postRepositoryMock.Object);
        _postRepositoryMock
            .Setup(x =>
                x.GetPagedAsync(skip, size, It.IsAny<CancellationToken>()))
            .ReturnsAsync(correctResult);
        _postRepositoryMock
            .Setup(x =>
                x.CountAsync(It.IsAny<CancellationToken>()))
            .ReturnsAsync(_postListMock.Count);

        var result = await handler.Handle(query, CancellationToken.None);

        _postRepositoryMock.Verify(x =>
            x.GetPagedAsync(skip, size, It.IsAny<CancellationToken>()), Times.Once);
        _postRepositoryMock.Verify(x =>
            x.CountAsync(It.IsAny<CancellationToken>()), Times.Once());
        result.IsSuccess.ShouldBeTrue();
        result.Value.TotalCount.ShouldBe(_postListMock.Count);
        result.Value.Size.ShouldBe(size);
        result.Value.PageNumber.ShouldBe(page);
        result.Value.PageNumber.ShouldBeGreaterThanOrEqualTo(1);
        result.Value.PageNumber.ShouldBeLessThanOrEqualTo(result.Value.TotalPages);
        result.Value.TotalPages.ShouldBe(GetTotalPages(size));
        result.Value.TotalPages.ShouldBeGreaterThanOrEqualTo(1);
        result.Value.Data.ShouldBeEquivalentTo(correctResult);
    }
}