using FluentValidation.TestHelper;
using Moq;
using SocialMedia.Application.Commands.DeletePost;
using SocialMedia.Application.Interfaces;
using SocialMedia.Domain.Entities;

namespace SocialMedia.UnitTests.Commands;

public class DeletePostTests()
{
    private readonly Mock<IPostRepository> _postRepositoryMock = new();
    private readonly DeletePostValidator _validator = new();
    private readonly Post _postMock = new()
    {
        Content = "post data",
        CreatedDate = default,
        UpdatedDate = null
    };
    
    [Fact]
    public async Task IdValidator_IdIsEmptyGuid_ReturnsFail()
    {
        var id = Guid.Empty;
        var command = new DeletePostCommand(id);

        var result = await _validator.TestValidateAsync(command);
        
        result.ShouldHaveValidationErrorFor(c => c.Id);
    }

    [Fact]
    public async Task Handle_IdNotExists_ReturnsFail()
    {
        var correctId = Guid.NewGuid();
        var incorrectId = Guid.NewGuid();
        var command = new DeletePostCommand(incorrectId);
        var handler = new DeletePostCommandHandler(_postRepositoryMock.Object);
        _postRepositoryMock
            .Setup(x => 
                x.GetByIdAsync(correctId)) // TODO SET CANCELLATION TOKEN
            .ReturnsAsync(_postMock);

        var result = await handler.Handle(command, default);
        
        Assert.NotEqual(correctId, incorrectId);
        _postRepositoryMock.Verify(x =>
            x.GetByIdAsync(incorrectId), Times.Once); // TODO SET CANCELLATION TOKEN
        _postRepositoryMock.Verify(x =>
            x.DeleteAsync(_postMock), Times.Never); // TODO SET CANCELLATION TOKEN
        Assert.True(result.IsFailed);
        Assert.True(result.HasError(x => x.Message == "")); // TODO SET THE ERROR MESSAGE
    }
    
    [Fact]
    public async Task Handle_IdCorrect_ReturnsSuccess()
    {
        var id = Guid.NewGuid();
        var command = new DeletePostCommand(id);
        var handler = new DeletePostCommandHandler(_postRepositoryMock.Object);
        _postRepositoryMock
            .Setup(x => 
                x.GetByIdAsync(id)) // TODO SET CANCELLATION TOKEN
            .ReturnsAsync(_postMock);

        var result = await handler.Handle(command, default);
        
        _postRepositoryMock.Verify(x =>
            x.GetByIdAsync(id), Times.Once); // TODO SET CANCELLATION TOKEN
        _postRepositoryMock.Verify(x =>
            x.DeleteAsync(_postMock), Times.Once); // TODO SET CANCELLATION TOKEN
        Assert.True(result.IsSuccess);
    }
}