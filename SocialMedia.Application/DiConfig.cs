using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SocialMedia.Application.Commands.CreatePost;
using SocialMedia.Application.Services;

namespace SocialMedia.Application;

public static class DiConfig
{
    public static void ConfigureServices(IServiceCollection services, ConfigurationManager config)
    {
        services.AddMediatR(config => { config.RegisterServicesFromAssembly(typeof(DiConfig).Assembly); });
        services.AddFluentValidationAutoValidation();
        services.AddFluentValidationClientsideAdapters();
        services.AddValidatorsFromAssemblyContaining<CreatePostValidator>();
        services.AddAutoMapper(typeof(CreatePostProfile));

        services.AddScoped<CurrentUserService>();
    }
}