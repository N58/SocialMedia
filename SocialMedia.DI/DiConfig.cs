using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace SocialMedia.DI;

public static class DiConfig
{
    public static void ConfigureServices(IServiceCollection services, ConfigurationManager config)
    {
        Infrastructure.DiConfig.ConfigureServices(services, config);
        Application.DiConfig.ConfigureServices(services, config);
    }

    public static void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        Application.DiConfig.Configure(app, env);
    }
}