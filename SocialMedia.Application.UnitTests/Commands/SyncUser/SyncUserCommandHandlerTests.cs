using AutoMapper;
using Moq;
using Shouldly;
using SocialMedia.Application.Commands.SyncUser;
using SocialMedia.Application.Interfaces;
using SocialMedia.Domain.Entities;

namespace SocialMedia.UnitTests.Commands.SyncUser;

public class SyncUserCommandHandlerTests
{
    private readonly IMapper _mapper;
    private readonly Mock<IUserRepository> _userRepositoryMock = new();

    public SyncUserCommandHandlerTests()
    {
        var configuration = new MapperConfiguration(cfg => { cfg.AddProfile<SyncUserProfile>(); });

        _mapper = configuration.CreateMapper();
    }

    [Fact]
    public async Task Handle_ValidRequest_ShouldCallRepositoryMethods()
    {
        var command = new SyncUserCommand("id", "John", "Doe", "john.doe@test.com", "https://test.com");
        var handler = new SyncUserCommandHandler(_userRepositoryMock.Object, _mapper);

        _userRepositoryMock
            .Setup(x => x.SyncUser(It.IsAny<User>(), It.IsAny<CancellationToken>()))
            .Returns(Task.CompletedTask);

        _userRepositoryMock
            .Setup(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()))
            .Returns(Task.CompletedTask);

        var result = await handler.Handle(command, CancellationToken.None);

        result.IsSuccess.ShouldBeTrue();
        _userRepositoryMock.Verify(x => x.SyncUser(It.IsAny<User>(), It.IsAny<CancellationToken>()), Times.Once);
        _userRepositoryMock.Verify(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
    }
}