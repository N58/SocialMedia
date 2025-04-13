using AutoMapper;
using SocialMedia.Application.Dtos;
using SocialMedia.Application.Dtos.Post;
using SocialMedia.Application.Dtos.User;
using SocialMedia.Domain.Common;
using SocialMedia.Domain.Entities;

namespace SocialMedia.API.Profiles;

public class ResponseProfile : Profile
{
    public ResponseProfile()
    {
        CreateMap<Post, PostDto>();
        CreateMap(typeof(Paged<>), typeof(PagedDto<>));
        CreateMap<User, UserDto>();
    }
}