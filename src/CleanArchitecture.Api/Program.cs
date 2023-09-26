using CleanArchitecture.Api.Extensions;
using CleanArchitecture.Api.Middleware;
using CleanArchitecture.Application.Services;
using CleanArchitecture.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Configure web host providers.
builder.Host.ConfigureLogging();

// Add services to the container.
builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddApiDocs();

builder.Services.AddCustomProblemDetails();

builder.Services.AddCustomHealthChecks(builder.Configuration);

// TODO: Context Items Service

builder.Services.AddHelloWorldRepository(builder.Configuration.GetConnectionString("HelloWorldDB"));

builder.Services.AddApplicatopmServices();

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseOpenApi();

app.UseCustomRequestLogging();

app.UsePingEndpointMiddleware();

// app.UseApiRequestHeadersMiddleware();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();