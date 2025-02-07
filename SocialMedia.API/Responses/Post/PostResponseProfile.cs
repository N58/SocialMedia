using AutoMapper;

namespace SocialMedia.API.Responses.Post;

public class PostResponseProfile : Profile
{
    public PostResponseProfile()
    {
        CreateMap<Domain.Entities.Post, PostResponse>();
    }
}