using AutoMapper;
using SocialMedia.API.Requests;
using SocialMedia.Application.Commands.CreatePost;
using SocialMedia.Application.Commands.DeletePost;
using SocialMedia.Application.Commands.UpdatePost;

namespace SocialMedia.API.Profiles;

public class RequestProfile : Profile
{
    public RequestProfile()
    {
        CreateMap<CreatePostRequest, CreatePostCommand>();
        CreateMap<UpdatePostRequest, UpdatePostCommand>();
        CreateMap<DeletePostRequest, DeletePostCommand>();
    }
}