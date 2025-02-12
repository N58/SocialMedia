using SocialMedia.API;
using SocialMedia.DI;
using SocialMedia.ServiceDefaults;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();
builder.Services.AddControllers();
builder.Services.AddOpenApi();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader();
    });
});

DiConfig.ConfigureServices(builder.Services, builder.Configuration);


var app = builder.Build();

SocialMedia.DI.DiConfig.Configure(app, app.Environment);

if (app.Environment.IsDevelopment()) app.MapOpenApi();

app.UseCors("AllowAll");

app.UseHttpsRedirection();

app.MapControllers();
app.Run();