using System.Reflection;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SchoolProject.Application.Behaviours;
using SchoolProject.Shared.Helpers;

namespace SchoolProject.Application;

public static class DependencyInjection
{

    public static IServiceCollection AddApplicationDependencies(this IServiceCollection services, IConfiguration configuration)
    {
        #region Binding Configuration
        services.Configure<EmailSettings>(configuration.GetSection("EmailSettings"));
        services.Configure<AccessTokensSettings>(configuration.GetSection("AccessTokensSettings"));
        #endregion
        // Register MediatR handlers
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(DependencyInjection).Assembly));

        // Register AutoMapper profiles (explicitly scanning this assembly)
        services.AddAutoMapper(cfg => cfg.AddMaps(typeof(DependencyInjection).Assembly));

        // Register pipeline behavior for automatic validation
        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
        return services;
    }
}
