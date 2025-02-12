using Moq;
using SocialMedia.Application.Interfaces;
using SocialMedia.Application.Queries.GetPost;
using SocialMedia.Domain.Constants;
using SocialMedia.Domain.Entities;

namespace SocialMedia.UnitTests.Queries.GetPost;

public class GetPostQueryHandlerTests
{
    private readonly Guid _correctGuid = Guid.NewGuid();
    private readonly Guid _incorrectGuid = Guid.NewGuid();
    private readonly Post _postMock;
    private readonly Mock<IPostRepository> _postRepositoryMock = new();

    public GetPostQueryHandlerTests()
    {
        _postMock = new Post
        {
            Id = _correctGuid,
            Content = "post data",
            CreatedDate = default,
            UpdatedDate = null
        };
    }

    [Fact]
    public async Task Handle_NotExistingPost_ReturnsFail()
    {
        var query = new GetPostQuery(_incorrectGuid);
        var queryHandler = new GetPostQueryHandler(_postRepositoryMock.Object);
        _postRepositoryMock
            .Setup(x =>
                x.GetByIdAsync(_correctGuid, It.IsAny<CancellationToken>()))
            .ReturnsAsync(_postMock);

        var result = await queryHandler.Handle(query, CancellationToken.None);

        _postRepositoryMock.Verify(x =>
            x.GetByIdAsync(_incorrectGuid, It.IsAny<CancellationToken>()), Times.Once);
        Assert.True(result.IsFailed);
        Assert.True(result.HasError(x => x.Message == Errors.Post.NoPostWithGivenId.Message));
    }

    [Fact]
    public async Task Handle_ExistingPost_ReturnsOk()
    {
        var query = new GetPostQuery(_correctGuid);
        var queryHandler = new GetPostQueryHandler(_postRepositoryMock.Object);
        _postRepositoryMock
            .Setup(x =>
                x.GetByIdAsync(_correctGuid, It.IsAny<CancellationToken>()))
            .ReturnsAsync(_postMock);

        var result = await queryHandler.Handle(query, CancellationToken.None);

        _postRepositoryMock.Verify(x =>
            x.GetByIdAsync(_correctGuid, It.IsAny<CancellationToken>()), Times.Once);
        Assert.True(result.IsSuccess);
        Assert.Equivalent(_postMock, result.Value);
    }
}