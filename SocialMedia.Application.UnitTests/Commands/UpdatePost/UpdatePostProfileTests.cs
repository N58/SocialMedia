using AutoMapper;
using Shouldly;
using SocialMedia.Application.Commands.UpdatePost;
using SocialMedia.Domain.Entities;

namespace SocialMedia.UnitTests.Commands.UpdatePost;

public class UpdatePostProfileTests
{
    private readonly IMapper _mapper;

    public UpdatePostProfileTests()
    {
        var configuration = new MapperConfiguration(cfg => { cfg.AddProfile<UpdatePostProfile>(); });

        _mapper = configuration.CreateMapper();
    }

    [Fact]
    public void Mapper_CommandMapping_MappedToPost()
    {
        var postId = Guid.NewGuid();
        const string initContent = "some random text";
        var command = new UpdatePostCommand(postId, initContent);

        var post = _mapper.Map<Post>(command);

        postId.ShouldBeEquivalentTo(post.Id);
        initContent.ShouldBeEquivalentTo(post.Content);
    }
}