using SocialMedia.ServiceDefaults;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();
builder.Services.AddControllers();
builder.Services.AddOpenApi();

SocialMedia.DI.DiConfig.ConfigureServices(builder.Services, builder.Configuration);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

SocialMedia.Application.DiConfig.Configure(app, app.Environment);

app.UseHttpsRedirection();

app.MapControllers();
app.Run();
