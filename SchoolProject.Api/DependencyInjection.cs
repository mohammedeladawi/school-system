using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using SchoolProject.Application.Helpers.ConfigBinders;
using SchoolProject.Application.Interfaces.IdentityServices;
using Serilog;

namespace SchoolProject.Api;


public static class DependencyInjection
{
    public static IServiceCollection AddApiDependencies(this IServiceCollection services, IConfiguration configuration)
    {
        #region Serialog Configurations 
        Log.Logger = new LoggerConfiguration()
            .ReadFrom.Configuration(configuration)
            .CreateLogger();
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


                var accessTokensSettings = configuration.GetSection("AccessTokensSettings")
                                                        .Get<AccessTokensSettings>();

                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,


                    ValidIssuer = accessTokensSettings?.Issuer,
                    ValidAudience = accessTokensSettings?.Audience,

                    IssuerSigningKey = new SymmetricSecurityKey(
                        Encoding.UTF8.GetBytes(accessTokensSettings!.Key!)),

                    ClockSkew = TimeSpan.Zero
                };

                options.Events = new JwtBearerEvents
                {
                    OnTokenValidated = async context =>
                    {
                        var userId = context.Principal!.FindFirstValue(ClaimTypes.NameIdentifier);
                        var tokenStamp = context.Principal!.FindFirstValue("security_stamp");

                        var userService = context.HttpContext.RequestServices.GetRequiredService<IUserManager>();
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
        // Todo: Update Authorization Logic
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