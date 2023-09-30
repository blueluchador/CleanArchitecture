using CleanArchitecture.Api.Extensions;
using CleanArchitecture.Api.Middleware;
using CleanArchitecture.Application.Services;
using CleanArchitecture.Infrastructure.ContextItems;
using CleanArchitecture.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Configure web host providers.
builder.Host.ConfigureLogging();

// Add services to the container.
builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();

if (builder.Environment.IsDevelopment())
{
    builder.Services.AddApiDocs();
}

builder.Services.AddCustomProblemDetails();

builder.Services.AddCustomHealthChecks(builder.Configuration);

builder.Services.AddContextItemsService();

builder.Services.AddHelloWorldRepository(builder.Configuration.GetConnectionString("HelloWorldDB"));

builder.Services.AddApplicationServices();

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseOpenApi();

app.UseCustomRequestLogging();

app.UseCorrelationIdMiddleware();

app.UsePingEndpointMiddleware();

app.UseRequestHeadersMiddleware(new RequestHeaderOptions { Headers = new[] { "Header-Example" } });

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

#pragma warning disable CA1050
// ReSharper disable once ClassNeverInstantiated.Global
// Declare partial class Program to reference from tests
public partial class Program { }
#pragma warning restore CA1050
