using Microsoft.Extensions.DependencyInjection;

namespace SchoolProject.Core;

public static class ModuleCoreDependencies
{

    public static IServiceCollection AddCoreDependencies(this IServiceCollection services)
    {
        // Register MediatR handlers
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(ModuleCoreDependencies).Assembly));

        // Register AutoMapper profiles
        services.AddAutoMapper(cfg => cfg.AddMaps(typeof(ModuleCoreDependencies).Assembly));

        return services;
    }
}
