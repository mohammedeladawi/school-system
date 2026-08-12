using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using SchoolProject.Core.Interfaces.Bases;
using SchoolProject.Core.Interfaces.Identities;
using SchoolProject.Core.Interfaces.Repositories;
using SchoolProject.Core.Interfaces.Services;
using SchoolProject.Data.Entities.Identities;
using SchoolProject.Infrastructure.Bases;
using SchoolProject.Infrastructure.Data;
using SchoolProject.Infrastructure.Identities;
using SchoolProject.Infrastructure.Repositories;
using SchoolProject.Infrastructure.Services;
using SchoolProject.Shared.Helpers;

namespace SchoolProject.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructureDependencies(this IServiceCollection services, IConfiguration configuration)
    {
        #region Repositories
        services.AddScoped<IStudentRepository, StudentRepository>();
        services.AddScoped<IInstructorRepository, InstructorRepository>();
        services.AddScoped<IDepartmentRepository, DepartmentRepository>();
        services.AddScoped<ISubjectRepository, SubjectRepository>();
        services.AddScoped<IRefreshTokenRepository, RefreshTokenRepository>();
        services.AddScoped<IPasswordResetCodeRepository, PasswordResetCodeRepository>();
        #endregion

        #region Bases
        services.AddScoped<IUnitOfWorkAsync, UnitOfWorkAsync>();
        #endregion

        #region DbContext

        services.AddDbContext<AppDbContext>(options =>
           options.UseSqlServer(configuration.GetConnectionString("dbcontext")));

        #endregion

        #region Services

        services.AddScoped<IEmailService, EmailService>();
        services.AddScoped<IFileService, FileService>();
        services.AddScoped<IAuthenticationService, AuthenticationService>();
        services.AddScoped<IAuthorizationService, AuthorizationService>();

        #endregion

        #region Identity

        services.AddIdentity<ApplicationUser, ApplicationRole>()
                .AddEntityFrameworkStores<AppDbContext>()
                .AddDefaultTokenProviders();

        services.AddScoped<IApplicationUserRepository, ApplicationUserRepository>();
        services.AddScoped<IApplicationRoleRepository, ApplicationRoleRepository>();

        #endregion

        #region Binding Configuration

        services.Configure<EmailSettings>(configuration.GetSection("EmailSettings"));

        #endregion

        #region JWT Authentication

        services.Configure<IdentityOptions>(options =>
               {
                   // Password settings.
                   options.Password.RequireDigit = true;
                   options.Password.RequireLowercase = true;
                   options.Password.RequireNonAlphanumeric = true;
                   options.Password.RequireUppercase = true;
                   options.Password.RequiredLength = 6;
                   options.Password.RequiredUniqueChars = 1;

                   // Lockout settings.
                   options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
                   options.Lockout.MaxFailedAccessAttempts = 5;
                   options.Lockout.AllowedForNewUsers = true;

                   // User settings.
                   options.User.AllowedUserNameCharacters =
                   "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
                   options.User.RequireUniqueEmail = true;
               });

        services
            .AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.RequireHttpsMetadata = true;
                options.SaveToken = true;


                var jwtSettings = configuration.GetSection("Jwt");
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,


                    ValidIssuer = jwtSettings["Issuer"],
                    ValidAudience = jwtSettings["Audience"],

                    IssuerSigningKey = new SymmetricSecurityKey(
                        Encoding.UTF8.GetBytes(jwtSettings["Key"]!)),

                    ClockSkew = TimeSpan.Zero
                };

                options.Events = new JwtBearerEvents
                {
                    OnTokenValidated = async context =>
                    {
                        var userId = context.Principal!.FindFirstValue(ClaimTypes.NameIdentifier);
                        var tokenStamp = context.Principal!.FindFirstValue("security_stamp");

                        var userService = context.HttpContext.RequestServices.GetRequiredService<IApplicationUserRepository>();
                        var user = await userService.GetByIdAsync(int.Parse(userId!));

                        if (user is null || user.SecurityStamp != tokenStamp)
                        {
                            context.Fail("Token is no longer valid - roles or credentials changed.");

                            context.Response.StatusCode = StatusCodes.Status403Forbidden;
                            context.Response.ContentType = "application/json";
                            await context.Response.WriteAsync("{\"error\":\"Token invalid due to role or credential change\"}");
                        }
                    }
                };
            });

        #endregion

        #region Authorization

        services.AddAuthorization(options =>
        {
            options.AddPolicy("User.GetPaginated", policy =>
            {
                policy.RequireClaim("Permission", "User.GetPaginated");
            });

            options.AddPolicy("User.ChangePassword", policy =>
            {
                policy.RequireClaim("Permission", "User.ChangePassword");
            });
        });

        #endregion

        #region Swagger
        // ======= ToDo: Move to api layer =======
        services.AddSwaggerGen(c =>
       {
           c.SwaggerDoc("v1", new() { Title = "SchoolProject API", Version = "v1" });
           c.AddSecurityDefinition(JwtBearerDefaults.AuthenticationScheme, new Microsoft.OpenApi.Models.OpenApiSecurityScheme
           {
               Description = "JWT Authorization header using the Bearer scheme. \r\n\r\n Enter 'Bearer' [space] and then your token in the text input below.\r\n\r\nExample: \"Bearer 12345abcdef\"",
               Name = "Authorization",
               In = Microsoft.OpenApi.Models.ParameterLocation.Header,
               Type = Microsoft.OpenApi.Models.SecuritySchemeType.ApiKey,
               Scheme = JwtBearerDefaults.AuthenticationScheme
           });

           c.AddSecurityRequirement(new Microsoft.OpenApi.Models.OpenApiSecurityRequirement
           {
                {
                    new Microsoft.OpenApi.Models.OpenApiSecurityScheme
                    {
                        Reference = new Microsoft.OpenApi.Models.OpenApiReference
                        {
                            Type = Microsoft.OpenApi.Models.ReferenceType.SecurityScheme,
                            Id = JwtBearerDefaults.AuthenticationScheme
                        }
                    },
                    Array.Empty<string>()
                }
           });
       });

        #endregion

        return services;
    }

}
