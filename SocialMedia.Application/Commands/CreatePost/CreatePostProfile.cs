using AutoMapper;
using SocialMedia.Domain.Entities;

namespace SocialMedia.Application.Commands.CreatePost;

public class CreatePostProfile : Profile
{
    public CreatePostProfile()
    {
        CreateMap<CreatePostCommand, Post>();
    }
}