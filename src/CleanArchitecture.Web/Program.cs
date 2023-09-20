using CleanArchitecture.Web.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Configure web host providers.
builder.Host.ConfigureLogging();

// Add services to the container.
builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddApiDocs();

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseOpenApi();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();