using Microsoft.EntityFrameworkCore;
using SchoolProject.Core;
using SchoolProject.Infrastructure;
using SchoolProject.Service;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container
builder.Services.AddControllers();

// Register Swagger services
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.AddInfrastructureDependencies(builder.Configuration)
    .AddCoreDependencies()
    .AddServiceDependencies();

var app = builder.Build();

// Configure middleware pipeline

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();