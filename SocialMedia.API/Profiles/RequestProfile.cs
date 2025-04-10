using AutoMapper;
using SocialMedia.API.Requests;
using SocialMedia.API.Responses;
using SocialMedia.API.Responses.Post;
using SocialMedia.API.Responses.User;
using SocialMedia.Application.Commands.CreatePost;
using SocialMedia.Application.Commands.DeletePost;
using SocialMedia.Application.Commands.UpdatePost;
using SocialMedia.Domain.Common;
using SocialMedia.Domain.Entities;

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