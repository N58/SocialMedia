using AutoMapper;
using SocialMedia.API.Responses;
using SocialMedia.API.Responses.Post;
using SocialMedia.Domain.Common;
using SocialMedia.Domain.Entities;

namespace SocialMedia.API.Profiles;

public class ResponseProfile : Profile
{
    public ResponseProfile()
    {
        CreateMap<Post, PostResponse>();
        CreateMap(typeof(Paged<>), typeof(PagedResponse<>));
    }
}