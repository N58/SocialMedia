using AutoMapper;
using Moq;
using Shouldly;
using SocialMedia.Application.Commands.CreatePost;
using SocialMedia.Application.Interfaces;
using SocialMedia.Domain.Entities;

namespace SocialMedia.UnitTests.Commands.CreatePost;

public class CreatePostCommandHandlerTests
{
    private readonly IMapper _mapper;
    private readonly Mock<IPostRepository> _postRepositoryMock = new();

    public CreatePostCommandHandlerTests()
    {
        var configuration = new MapperConfiguration(cfg => { cfg.AddProfile<CreatePostProfile>(); });

        _mapper = configuration.CreateMapper();
    }

    [Fact]
    public async Task Handle_PostCorrect_ShouldCallRepository()
    {
        var command = new CreatePostCommand("some random text");
        var handler = new CreatePostCommandHandler(_postRepositoryMock.Object, _mapper);
        _postRepositoryMock
            .Setup(x =>
                x.AddAsync(It.IsAny<Post>(), It.IsAny<CancellationToken>()));

        var result = await handler.Handle(command, CancellationToken.None);

        result.IsSuccess.ShouldBeTrue();
        _postRepositoryMock.Verify(x => x.AddAsync(It.IsAny<Post>(), It.IsAny<CancellationToken>()), Times.Once);
        _postRepositoryMock.Verify(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
    }
}