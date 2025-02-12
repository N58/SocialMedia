using AutoMapper;
using FluentValidation.TestHelper;
using Moq;
using SocialMedia.Application.Commands.CreatePost;
using SocialMedia.Application.Interfaces;
using SocialMedia.Domain.Constants;
using SocialMedia.Domain.Entities;

namespace SocialMedia.UnitTests;

public class CreatePostTests
{
    private readonly IMapper _mapper;
    private readonly Mock<IPostRepository> _postRepositoryMock = new();
    private readonly CreatePostValidator _validator = new();

    public CreatePostTests()
    {
        var configuration = new MapperConfiguration(cfg => { cfg.AddProfile<CreatePostProfile>(); });
    
        _mapper = configuration.CreateMapper();
    }

    [Fact]
    public void Mapper_CommandMapping_MappedToPost()
    {
        const string initContent = "some random text";
        var command = new CreatePostCommand(initContent);

        var post = _mapper.Map<Post>(command);

        Assert.Equal(initContent, post.Content);
    }

    [Theory]
    [InlineData(" ")]
    [InlineData("")]
    [InlineData(null)]
    public async Task ContentValidator_ContentIsEmpty_ReturnsFail(string content)
    {
        var command = new CreatePostCommand(content);

        var result = await _validator.TestValidateAsync(command);

        result.ShouldHaveValidationErrorFor(c => c.Content);
    }

    [Fact]
    public async Task ContentValidator_ContentHasMinimumLength_ReturnsFail()
    {
        var content = new string('a', 2);
        var command = new CreatePostCommand(content);

        var result = await _validator.TestValidateAsync(command);

        result.ShouldHaveValidationErrorFor(c => c.Content);
    }

    [Fact]
    public async Task ContentValidator_ContentExceedsMaxLength_ReturnsFail()
    {
        var content = new string('a', 1001);
        var command = new CreatePostCommand(content);

        var result = await _validator.TestValidateAsync(command);

        result.ShouldHaveValidationErrorFor(c => c.Content);
    }

    [Fact]
    public async Task ContentValidator_ContentIsValid_ReturnsOk()
    {
        var content = new string('a', 1000);
        var command = new CreatePostCommand(content);

        var result = await _validator.TestValidateAsync(command);
        result.ShouldNotHaveValidationErrorFor(c => c.Content);
    }

    [Fact]
    public async Task Handle_PostCorrect_ShouldCallRepository()
    {
        var command = new CreatePostCommand("some random text");
        var handler = new CreatePostCommandHandler(_postRepositoryMock.Object, _mapper);
        _postRepositoryMock
            .Setup(x =>
                x.AddAsync(It.IsAny<Post>(), It.IsAny<CancellationToken>()));

        await handler.Handle(command, default);

        _postRepositoryMock.Verify(x => x.AddAsync(It.IsAny<Post>(), It.IsAny<CancellationToken>()), Times.Once);
        _postRepositoryMock.Verify(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
    }
}