var builder = DistributedApplication.CreateBuilder(args);

var api = builder.AddProject<Projects.SocialMedia_API>("API")
    .WithExternalHttpEndpoints();

builder.AddNpmApp("Svelte", "../SocialMedia.SvelteKit", scriptName: "dev")
    .WithReference(api)
    .WithHttpEndpoint(env: "PORT")
    .WithExternalHttpEndpoints()
    .PublishAsDockerFile();

builder.Build().Run();