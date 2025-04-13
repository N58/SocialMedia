using AutoMapper;
using Moq;
using Shouldly;
using SocialMedia.Application.Commands.UpdatePost;
using SocialMedia.Application.Interfaces;
using SocialMedia.Domain.Entities;

namespace SocialMedia.UnitTests.Commands.UpdatePost;

public class UpdatePostCommandHandlerTests
{
    private readonly Guid _correctGuid = Guid.NewGuid();
    private readonly Guid _incorrectGuid = Guid.NewGuid();
    private readonly IMapper _mapper;
    private readonly Post _postMock;
    private readonly Mock<IPostRepository> _postRepositoryMock = new();

    public UpdatePostCommandHandlerTests()
    {
        var configuration = new MapperConfiguration(cfg => { cfg.AddProfile<UpdatePostProfile>(); });

        _mapper = configuration.CreateMapper();

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
        var command = new UpdatePostCommand
        {
            Id = _incorrectGuid,
            Content = "new content",
            AuthorId = "12345"
        };
        var handler = new UpdatePostCommandHandler(_postRepositoryMock.Object, _mapper);
        _postRepositoryMock.Setup(x =>
                x.GetEntityByIdAsync(_correctGuid, It.IsAny<CancellationToken>()))
            .ReturnsAsync(_postMock);

        var result = await handler.Handle(command, CancellationToken.None);

        result.IsFailed.ShouldBeTrue();
        _postRepositoryMock.Verify(x => x.GetEntityByIdAsync(command.Id, It.IsAny<CancellationToken>()), Times.Once);
        _postRepositoryMock.Verify(x => x.UpdateAsync(It.IsAny<Post>()), Times.Never);
        _postRepositoryMock.Verify(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Never);
    }

    [Fact]
    public async Task Handle_UpdateCorrect_ReturnsSuccess()
    {
        var command = new UpdatePostCommand
        {
            Id = _correctGuid,
            Content = "new content",
            AuthorId = "12345"
        };
        var handler = new UpdatePostCommandHandler(_postRepositoryMock.Object, _mapper);
        var mappedPost = _mapper.Map<Post>(command);
        Post capturedPost = null!;
        _postRepositoryMock.Setup(x =>
                x.GetEntityByIdAsync(command.Id, It.IsAny<CancellationToken>()))
            .ReturnsAsync(_postMock);
        _postRepositoryMock
            .Setup(x =>
                x.UpdateAsync(It.IsAny<Post>()))
            .Callback<Post>(post => capturedPost = post);

        var result = await handler.Handle(command, CancellationToken.None);

        result.IsSuccess.ShouldBeTrue();
        _postRepositoryMock.Verify(x => x.GetEntityByIdAsync(command.Id, It.IsAny<CancellationToken>()), Times.Once);
        _postRepositoryMock.Verify(x => x.UpdateAsync(capturedPost), Times.Once);
        _postRepositoryMock.Verify(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
        mappedPost.CreatedDate = capturedPost.CreatedDate; // ignore testing this value
        capturedPost.ShouldBeEquivalentTo(mappedPost);
    }
}