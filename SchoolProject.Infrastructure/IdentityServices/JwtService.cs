using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;
using SchoolProject.Application.Helpers.ConfigBinders;
using SchoolProject.Application.Interfaces.IdentityServices;
using SchoolProject.Application.Interfaces.Repositories;
using SchoolProject.Application.Interfaces.Services;
using SchoolProject.Domain.Entities;
using SchoolProject.Domain.Entities.Identities;
using SchoolProject.Infrastructure.Data;

namespace SchoolProject.Infrastructure.IdentityServices;

public class JwtService : IJwtService
{
    #region Private Fields
    private readonly JwtSettings _JwtSettings;
    private readonly IAuthorizationService _authorizationService;

    #endregion

    #region Constructors
    public JwtService(
        IOptions<JwtSettings> JwtSettings,
        IAuthorizationService authorizationService)
    {
        _JwtSettings = JwtSettings.Value;
        _authorizationService = authorizationService;
    }

    #endregion


    #region Public Methods
    public async Task<string> GenerateJwtTokenAsync(ApplicationUser user)
    {
        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(ClaimTypes.Name, user.UserName!),
            new Claim(ClaimTypes.Email, user.Email!),
            new Claim("security_stamp", user.SecurityStamp ?? string.Empty)
        };

        var roles = await _authorizationService.GetUserRolesAsync(user.Id);
        foreach (var role in roles)
            claims.Add(new Claim(ClaimTypes.Role, role));

        var permissions = await _authorizationService.GetUserPermissionsAsync(user.Id);
        foreach (var permission in permissions)
            claims.Add(new Claim("Permission", permission));


        var key = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(_JwtSettings.Key!)
        );

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Issuer = _JwtSettings.Issuer,
            Audience = _JwtSettings.Audience,
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.UtcNow.AddMinutes(
                Convert.ToDouble(_JwtSettings.AccessTokenDurationInMinutes)
            ),
            SigningCredentials = new SigningCredentials(
                key,
                SecurityAlgorithms.HmacSha256)
        };

        var handler = new JsonWebTokenHandler();

        return handler.CreateToken(tokenDescriptor);
    }

    #endregion
}
