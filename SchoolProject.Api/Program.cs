using Microsoft.AspNetCore.Localization;
using Microsoft.EntityFrameworkCore;
using SchoolProject.Api.Middlewares;
using SchoolProject.Core;
using SchoolProject.Infrastructure;
using SchoolProject.Service;
using System.Globalization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container
builder.Services.AddControllers();

// Register Swagger services
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.AddInfrastructureDependencies(builder.Configuration)
    .AddCoreDependencies()
    .AddServiceDependencies();

builder.Services.AddLocalization(options => options.ResourcesPath = "");

var app = builder.Build();


#region Localization Configurations

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

// Enable localization middleware
app.UseRequestLocalization(localizationOptions);

#endregion


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

app.UseAuthorization();

app.MapControllers();

app.Run();