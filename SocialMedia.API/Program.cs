using SocialMedia.API;
using SocialMedia.ServiceDefaults;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();
builder.Services.AddControllers();
builder.Services.AddOpenApi();

SocialMedia.DI.DiConfig.ConfigureServices(builder.Services, builder.Configuration);


var app = builder.Build();

SocialMedia.DI.DiConfig.Configure(app, app.Environment);

if (app.Environment.IsDevelopment()) app.MapOpenApi();

app.UseHttpsRedirection();

app.MapControllers();
app.Run();