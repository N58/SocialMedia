using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Google;
using SocialMedia.API.Middlewares;
using SocialMedia.API.Options;
using SocialMedia.API.Profiles;
using SocialMedia.API.Validation;
using SocialMedia.DI;
using SocialMedia.ServiceDefaults;

var builder = WebApplication.CreateBuilder(args);

var authConfig = builder.Configuration.GetSection("Authentication").Get<AuthOptions>();
    
builder.AddServiceDefaults();
builder.Services.AddControllers();
builder.Services.AddOpenApi();
builder.Services.AddMediatR(config =>
{
    config.RegisterServicesFromAssembly(typeof(DiConfig).Assembly);
    config.AddOpenBehavior(typeof(ValidationBehavior<,>));
});
builder.Services.AddAutoMapper(typeof(ResponseProfile));

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader();
    });
});

builder.Services.AddAuthentication(options =>
    {
        options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = GoogleDefaults.AuthenticationScheme;
    })
    .AddCookie()
    .AddGoogle(options =>
    {
        options.ClientId = authConfig!.Google.ClientId;
        options.ClientSecret = authConfig.Google.ClientSecret;
        options.ClaimActions.MapJsonKey("picture", "picture", "url");
    });

DiConfig.ConfigureServices(builder.Services, builder.Configuration);


var app = builder.Build();

app.UseMiddleware<ValidationExceptionHandlingMiddleware>();

if (app.Environment.IsDevelopment()) app.MapOpenApi();

app.UseCors("AllowAll");

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.UseMiddleware<AttachUserMiddleware>();


app.MapControllers();
app.Run();