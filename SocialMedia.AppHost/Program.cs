using Projects;

var builder = DistributedApplication.CreateBuilder(args);

builder.AddProject<SocialMedia_API>("API");

builder.Build().Run();