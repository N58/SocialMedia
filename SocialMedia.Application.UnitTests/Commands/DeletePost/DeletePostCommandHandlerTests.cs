using Moq;
using Shouldly;
using SocialMedia.Application.Commands.DeletePost;
using SocialMedia.Application.Interfaces;
using SocialMedia.Domain.Constants;
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
            UpdatedDate = null,
            AuthorId = "12345",
            Author = null!
        };
    }

    [Fact]
    public async Task Handle_IdNotExists_ReturnsFail()
    {
        var command = new DeletePostCommand
        {
            Id = _incorrectGuid,
            AuthorId = null!
        };
        var handler = new DeletePostCommandHandler(_postRepositoryMock.Object);
        _postRepositoryMock
            .Setup(x =>
                x.GetEntityByIdAsync(_correctGuid, It.IsAny<CancellationToken>()))
            .ReturnsAsync(_postMock);

        var result = await handler.Handle(command);

        _correctGuid.ShouldNotBe(_incorrectGuid);
        _postRepositoryMock.Verify(x =>
            x.GetEntityByIdAsync(_incorrectGuid, It.IsAny<CancellationToken>()), Times.Once);
        _postRepositoryMock.Verify(x =>
            x.DeleteAsync(_postMock, It.IsAny<CancellationToken>()), Times.Never);
        result.IsFailed.ShouldBeTrue();
        result.HasError(x => x.Message == Errors.Post.NoPostWithGivenId.Message).ShouldBeTrue();
    }

    [Fact]
    public async Task Handle_IdCorrect_ReturnsSuccess()
    {
        var command = new DeletePostCommand
        {
            Id = _correctGuid,
            AuthorId = "12345"
        };
        var handler = new DeletePostCommandHandler(_postRepositoryMock.Object);
        _postRepositoryMock
            .Setup(x =>
                x.GetEntityByIdAsync(_correctGuid, It.IsAny<CancellationToken>()))
            .ReturnsAsync(_postMock);

        var result = await handler.Handle(command);

        _postRepositoryMock.Verify(x =>
            x.GetEntityByIdAsync(_correctGuid, It.IsAny<CancellationToken>()), Times.Once);
        _postRepositoryMock.Verify(x =>
            x.DeleteAsync(_postMock, It.IsAny<CancellationToken>()), Times.Once);
        result.IsSuccess.ShouldBeTrue();
    }
}