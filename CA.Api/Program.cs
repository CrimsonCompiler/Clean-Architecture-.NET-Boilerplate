using CA.Application.Common.Mappings;
using CA.Application.Features.Products.Commands;
using CA.Domain.Interfaces;
using CA.Infrastructure.Data;
using CA.Infrastructure.Repositories;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

// Database Configuration
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

// Dependency Injection (Generic Repository)
builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));

// MediatR (CQRS)
builder.Services.AddMediatR(cfg => 
    cfg.RegisterServicesFromAssembly(typeof(CreateProductCommand).Assembly));

// FluentValidation
builder.Services.AddValidatorsFromAssembly(typeof(CreateProductCommandValidator).Assembly);

// AutoMapper 
builder.Services.AddAutoMapper(typeof(MappingProfile).Assembly);

// Controllers & API Explorer
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddOpenApi();

// CORS Policy
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader();
    });
});

var app = builder.Build();

// Middleware
app.UseCors("AllowAll");

// Scalar/Swagger
app.MapOpenApi();
app.MapScalarApiReference(options =>
{
    options.Title = "Clean Architecture API";
});

// Health check
app.MapGet("/", () => "Clean Architecture API is running! 🚀");

// Auto-create database
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    db.Database.EnsureCreated();
}

app.MapControllers();
app.Run();