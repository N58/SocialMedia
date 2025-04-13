using FluentValidation.TestHelper;
using SocialMedia.Application.Commands.DeletePost;

namespace SocialMedia.UnitTests.Commands.DeletePost;

public class DeletePostValidatorTests
{
    private readonly DeletePostValidator _validator = new();

    [Fact]
    public async Task IdValidator_IdIsEmptyGuid_ReturnsFail()
    {
        var id = Guid.Empty;
        var command = new DeletePostCommand
        {
            Id = id,
            AuthorId = "12345"
        };

        var result = await _validator.TestValidateAsync(command);

        result.ShouldHaveValidationErrorFor(c => c.Id);
    }
}