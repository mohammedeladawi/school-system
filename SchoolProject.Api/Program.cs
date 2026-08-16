using System.Globalization;
using Microsoft.AspNetCore.Localization;
using Microsoft.EntityFrameworkCore;
using SchoolProject.Api.Middlewares;
using SchoolProject.Application;
using Serilog;
using SchoolProject.Infrastructure;
using SchoolProject.Api;

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog();

// Add services to the container
builder.Services.AddControllers();

// Register Swagger services
builder.Services.AddEndpointsApiExplorer();

builder.Services
    .AddApiDependencies(builder.Configuration)
    .AddApplicationDependencies(builder.Configuration)
    .AddInfrastructureDependencies(builder.Configuration);

#region Localization Configurations
builder.Services.AddLocalization();

// Supported cultures
var supportedCultures = new[]
{
    new CultureInfo("en"),
    new CultureInfo("ar")
};

// Configure localization options
var localizationOptions = new RequestLocalizationOptions
{
    DefaultRequestCulture = new RequestCulture("en"),
    SupportedCultures = supportedCultures,
    SupportedUICultures = supportedCultures
};
#endregion

var app = builder.Build();

app.UseSerilogRequestLogging();

// Enable localization middleware
app.UseRequestLocalization(localizationOptions);


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<ErrorHandlerMiddleware>();

if (!app.Environment.IsDevelopment())
{
    app.UseHttpsRedirection();
}

app.UseStaticFiles();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();