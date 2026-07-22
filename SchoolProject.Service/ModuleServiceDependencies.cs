using Microsoft.Extensions.DependencyInjection;
using SchoolProject.Service.Abstracts;
using SchoolProject.Service.Implementations;

namespace SchoolProject.Service;

public static class ModuleServiceDependencies
{
    public static IServiceCollection AddServiceDependencies(this IServiceCollection services)
    {
        services.AddScoped<IStudentService, StudentService>();
        services.AddScoped<IDepartmentService, DepartmentService>();
        services.AddScoped<IApplicationUserService, ApplicationUserService>();
        services.AddScoped<IAuthenticationService, AuthenticationService>();
        services.AddScoped<IApplicationRoleService, ApplicationRoleService>();
        services.AddScoped<IAuthorizationService, AuthorizationService>();
        services.AddTransient<IEmailService, EmailService>();


        return services;
    }

}
