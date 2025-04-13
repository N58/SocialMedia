using Moq;
using Shouldly;
using SocialMedia.Application.Dtos.Post;
using SocialMedia.Application.Interfaces;
using SocialMedia.Application.Queries.GetPost;
using SocialMedia.Domain.Constants;

namespace SocialMedia.UnitTests.Queries.GetPost;

public class GetPostQueryHandlerTests
{
    private readonly Guid _correctGuid = Guid.NewGuid();
    private readonly Guid _incorrectGuid = Guid.NewGuid();
    private readonly PostDto _postMock;
    private readonly Mock<IPostRepository> _postRepositoryMock = new();

    public GetPostQueryHandlerTests()
    {
        _postMock = new PostDto
        {
            Id = _correctGuid,
            Content = "post data",
            CreatedDate = default,
            AuthorGivenName = "John",
            AuthorFamilyName = "Doe",
            AuthorImage = "image1.jpg"
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
        result.IsFailed.ShouldBeTrue();
        result.HasError(x => x.Message == Errors.Post.NoPostWithGivenId.Message).ShouldBeTrue();
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
        result.IsSuccess.ShouldBeTrue();
        _postMock.ShouldBeEquivalentTo(result.Value);
    }
}