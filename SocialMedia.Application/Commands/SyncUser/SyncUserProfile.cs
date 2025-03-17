using AutoMapper;
using SocialMedia.Domain.Entities;

namespace SocialMedia.Application.Commands.SyncUser;

public class SyncUserProfile : Profile
{
    public SyncUserProfile()
    {
        CreateMap<SyncUserCommand, User>();
    }
}