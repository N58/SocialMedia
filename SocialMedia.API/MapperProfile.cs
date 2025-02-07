using AutoMapper;
using SocialMedia.API.Converters;

namespace SocialMedia.API;

public class MapperProfile : Profile
{
    public MapperProfile()
    {
        CreateMap<DateTimeOffset, DateTime>().ConvertUsing<DateTimeTypeConverter>();
    }
}