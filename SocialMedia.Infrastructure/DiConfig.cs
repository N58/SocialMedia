using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SocialMedia.Application.Interfaces;
using SocialMedia.Infrastructure.Repositories;

namespace SocialMedia.Infrastructure;

public static class DiConfig
{
    public static void ConfigureServices(IServiceCollection services, ConfigurationManager config)
    {
        var dbConnectionString = config.GetConnectionString("DefaultConnection");
        services.AddDbContext<AppDbContext>(options =>
            options.UseSqlServer(dbConnectionString));

        services.AddScoped<IPostRepository, PostRepository>();
    }
}