using AutoMapper;
using Shouldly;
using SocialMedia.Application.Commands.CreatePost;
using SocialMedia.Domain.Entities;

namespace SocialMedia.UnitTests.Commands.CreatePost;

public class CreatePostProfileTests
{
    private readonly IMapper _mapper;

    public CreatePostProfileTests()
    {
        var configuration = new MapperConfiguration(cfg => { cfg.AddProfile<CreatePostProfile>(); });

        _mapper = configuration.CreateMapper();
    }

    [Fact]
    public void Mapper_CommandMapping_MappedToPost()
    {
        const string initContent = "some random text";
        var command = new CreatePostCommand
        {
            Content = initContent,
            AuthorId = "12345"
        };

        var post = _mapper.Map<Post>(command);

        initContent.ShouldBeEquivalentTo(post.Content);
    }
}