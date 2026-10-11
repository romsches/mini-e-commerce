using Ecommerce.Api.Middleware;
using Ecommerce.Application;
using Ecommerce.Application.Validators;
using Ecommerce.Infrastructure;
using Ecommerce.Infrastructure.Persistence;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.EntityFrameworkCore;
using Serilog;

/**
 * Enterprise Application Entry Point (`Program.cs`).
 * Configures the complete dependency injection graph, Serilog telemetry pipeline,
 * CORS policies, Clean Architecture layers, middleware, and runtime EF Core database migrations.
 * Standards: Senior Backend Architecture (.NET / C# - 2026)
 */

var builder = WebApplication.CreateBuilder(args);

// 1. Centralized Serilog configuration with third-party log filtering
Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Information()
    .MinimumLevel.Override("Microsoft.AspNetCore", Serilog.Events.LogEventLevel.Information)
    .Filter.ByExcluding(e => e.MessageTemplate.Text.Contains("Lucky Penny"))
    .WriteTo.Console()
    .CreateLogger();

builder.Host.UseSerilog();

// CORS Policy Configuration for Single Page Application (SPA)
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend", policy =>
    {
        policy.WithOrigins("http://localhost:5173")
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

// Core API Services & Documentation
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Clean Architecture Layers Registration
builder.Services.AddApplication();
builder.Services.AddInfrastructure(builder.Configuration);

// FluentValidation Pipeline Setup
builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddValidatorsFromAssemblyContaining<CreateProductDtoValidator>();

var app = builder.Build();

// CORS Middleware (Must precede routing, authorization, and controllers)
app.UseCors("AllowFrontend");

// Global Exception Handling Middleware
app.UseMiddleware<GlobalExceptionHandlerMiddleware>();

// HTTP Request Pipeline Configuration (Swagger enabled for containerized environments)
app.UseSwagger();
app.UseSwaggerUI();

app.UseAuthorization();
app.MapControllers();

// Automatic EF Core Database Migration on Container Startup
using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<EcommerceDbContext>();
    await dbContext.Database.MigrateAsync();
}

try
{
    Log.Information("Starte E-Commerce Web-Anwendung...");
    app.Run();
}
catch (Exception ex)
{
    Log.Fatal(ex, "Anwendung unerwartet beendet.");
    throw;
}
finally
{
    Log.CloseAndFlush();
}