using FluentValidation.TestHelper;
using SocialMedia.Application.Commands.UpdatePost;

namespace SocialMedia.UnitTests.Commands.UpdatePost;

public class UpdatePostValidatorTests
{
    private readonly UpdatePostValidator _validator = new();

    public static IEnumerable<object[]> InvalidContentData =>
        new List<object[]>
        {
            new object[] { " " },
            new object[] { "" },
            new object[] { new string('a', 2) },
            new object[] { new string('a', 1001) }
        };

    [Fact]
    public async Task IdValidator_EmptyGuid_ShouldHaveValidationError()
    {
        var command = new UpdatePostCommand(Guid.Empty, "test test");

        var result = await _validator.TestValidateAsync(command);

        result.ShouldHaveValidationErrorFor(x => x.Id);
    }

    [Theory]
    [MemberData(nameof(InvalidContentData))]
    public async Task ContentValidator_InvalidContent_ShouldHaveValidationError(string content)
    {
        var command = new UpdatePostCommand(Guid.NewGuid(), content);

        var result = await _validator.TestValidateAsync(command);

        result.ShouldHaveValidationErrorFor(c => c.Content);
    }

    [Theory]
    [InlineData(3)]
    [InlineData(500)]
    [InlineData(1000)]
    public async Task ContentValidator_ValidContent_ShouldNotHaveValidationError(int length)
    {
        var content = new string('a', length);
        var command = new UpdatePostCommand(Guid.NewGuid(), content);

        var result = await _validator.TestValidateAsync(command);

        result.ShouldNotHaveValidationErrorFor(c => c.Content);
    }
}