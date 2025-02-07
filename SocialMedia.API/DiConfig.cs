namespace SocialMedia.API;

public static class DiConfig
{
    public static void ConfigureServices(IServiceCollection services, ConfigurationManager config)
    {
        services.AddAutoMapper(typeof(MapperProfile));
    }
}