using AutoMapper;
using SocialMedia.Domain.Entities;

namespace SocialMedia.Application.Commands.UpdatePost;

public class UpdatePostProfile : Profile
{
    public UpdatePostProfile()
    {
        CreateMap<UpdatePostCommand, Post>();
    }
}