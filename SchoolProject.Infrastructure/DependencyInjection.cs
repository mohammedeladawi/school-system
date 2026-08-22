using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using SchoolProject.Application.Interfaces.Bases;
using SchoolProject.Application.Interfaces.IdentityServices;
using SchoolProject.Application.Interfaces.Repositories;
using SchoolProject.Application.Interfaces.Services;
using SchoolProject.Domain.Entities.Identities;
using SchoolProject.Infrastructure.Bases;
using SchoolProject.Infrastructure.Data;
using SchoolProject.Infrastructure.IdentityServices;
using SchoolProject.Infrastructure.Repositories;
using SchoolProject.Infrastructure.Services;

namespace SchoolProject.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructureDependencies(this IServiceCollection services, IConfiguration configuration)
    {
        #region Repositories
        services.AddScoped<IStudentManager, StudentManager>();
        services.AddScoped<IInstructorManager, InstructorManager>();
        services.AddScoped<IDepartmentRepository, DepartmentRepository>();
        services.AddScoped<ISubjectRepository, SubjectRepository>();
        services.AddScoped<IRefreshTokenRepository, RefreshTokenRepository>();
        services.AddScoped<IPasswordResetCodeRepository, PasswordResetCodeRepository>();
        #endregion

        #region Bases
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        #endregion

        #region DbContext
        services.AddDbContext<AppDbContext>(options =>
           options.UseSqlServer(configuration.GetConnectionString("dbcontext")));
        #endregion

        #region Services
        services.AddScoped<IEmailService, EmailService>();
        services.AddScoped<IFileService, FileService>();
        services.AddScoped<IJwtService, JwtService>();
        services.AddScoped<IAuthorizationService, AuthorizationService>();
        #endregion

        #region Identity
        services.AddIdentity<ApplicationUser, ApplicationRole>()
                .AddEntityFrameworkStores<AppDbContext>()
                .AddDefaultTokenProviders();

        services.AddScoped<IUserManager, UserManager>();
        services.AddScoped<IRoleManager, RoleManager>();
        #endregion

        return services;
    }

}
