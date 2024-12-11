using Maternity.Repository.DbContexts;
using Maternity.Repository.Extensions;
using Maternity.Services.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection")
    ?? Environment.GetEnvironmentVariable("ConnectionStrings__DefaultConnection");
builder.Services.AddApplicationRepositories(builder.Configuration, connectioDb: connectionString);

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c => { c.SwaggerDoc("v1", new OpenApiInfo { Title = "MaternityApi", Version = "v1" }); });

builder.Services.AddHealthChecks();
builder.Services.AddApplicationServices();
builder.Logging.ClearProviders();
builder.Logging.AddConsole();

var app = builder.Build();

using var scope = app.Services.CreateScope();
using var dbContext = scope.ServiceProvider.GetRequiredService<MaternityContext>();

await dbContext.Database.EnsureCreatedAsync();

// Configure the HTTP request pipeline.

app.UseSwagger();
app.UseSwaggerUI();
app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "MaternityApi v1"));

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapGet("/health", () => Results.Ok("API is healthy!"));

app.MapControllers();

app.Run();