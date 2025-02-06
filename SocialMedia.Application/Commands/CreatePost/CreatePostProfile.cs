using AutoMapper;
using SocialMedia.Application.Commands.CreatePost;
using SocialMedia.Domain.Entities;

namespace SocialMedia.API;

public class MapperProfile : Profile
{
    public MapperProfile()
    {
        CreateMap<CreatePostCommand, Post>();
    }
}