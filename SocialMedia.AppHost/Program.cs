using Projects;

var builder = DistributedApplication.CreateBuilder(args);

var api = builder.AddProject<SocialMedia_API>("API")
    .WithExternalHttpEndpoints();

builder.AddNpmApp("Svelte", "../SocialMedia.SvelteKit", "dev")
    .WithReference(api)
    .WithHttpEndpoint(env: "PORT")
    .WithExternalHttpEndpoints()
    .PublishAsDockerFile();

builder.Build().Run();