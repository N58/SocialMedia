using Moq;
using SocialMedia.Application.Commands.DeletePost;
using SocialMedia.Application.Interfaces;
using SocialMedia.Domain.Entities;

namespace SocialMedia.UnitTests.Commands.DeletePost;

public class DeletePostCommandHandlerTests
{
    private readonly Guid _correctGuid = Guid.NewGuid();
    private readonly Guid _incorrectGuid = Guid.NewGuid();
    private readonly Post _postMock;
    private readonly Mock<IPostRepository> _postRepositoryMock = new();

    public DeletePostCommandHandlerTests()
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
    public async Task Handle_IdNotExists_ReturnsFail()
    {
        var command = new DeletePostCommand(_incorrectGuid);
        var handler = new DeletePostCommandHandler(_postRepositoryMock.Object);
        _postRepositoryMock
            .Setup(x =>
                x.GetByIdAsync(_correctGuid, It.IsAny<CancellationToken>()))
            .ReturnsAsync(_postMock);

        var result = await handler.Handle(command, default);

        Assert.NotEqual(_correctGuid, _incorrectGuid);
        _postRepositoryMock.Verify(x =>
            x.GetByIdAsync(_incorrectGuid, It.IsAny<CancellationToken>()), Times.Once);
        _postRepositoryMock.Verify(x =>
            x.DeleteAsync(_postMock, It.IsAny<CancellationToken>()), Times.Never);
        Assert.True(result.IsFailed);
        Assert.True(result.HasError(x => x.Message == "")); // TODO SET THE ERROR MESSAGE
    }

    [Fact]
    public async Task Handle_IdCorrect_ReturnsSuccess()
    {
        var command = new DeletePostCommand(_correctGuid);
        var handler = new DeletePostCommandHandler(_postRepositoryMock.Object);
        _postRepositoryMock
            .Setup(x =>
                x.GetByIdAsync(_correctGuid, It.IsAny<CancellationToken>()))
            .ReturnsAsync(_postMock);

        var result = await handler.Handle(command, default);

        _postRepositoryMock.Verify(x =>
            x.GetByIdAsync(_correctGuid, It.IsAny<CancellationToken>()), Times.Once);
        _postRepositoryMock.Verify(x =>
            x.DeleteAsync(_postMock, It.IsAny<CancellationToken>()), Times.Once);
        Assert.True(result.IsSuccess);
    }
}