var builder = DistributedApplication.CreateBuilder(args);

builder.AddProject<Projects.SocialMedia_API>("API");

builder.Build().Run();