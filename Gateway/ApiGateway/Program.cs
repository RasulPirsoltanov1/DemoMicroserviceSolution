using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration.SetBasePath(builder.Environment.ContentRootPath)
    .AddJsonFile("ocelot.json", false, true)
    .AddEnvironmentVariables();

// Configure Ocelot
builder.Services.AddOcelot(builder.Configuration);

var app = builder.Build();

// Map your routes or define other middleware as needed
app.MapGet("/", () => "Hello World!");

// Use Ocelot Middleware
app.UseOcelot().Wait();

app.Run();
