using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;
using SchoolProject.Data.Entities.Identities;
using SchoolProject.Service.Abstracts;

namespace SchoolProject.Service.Implementations;

public class AuthenticationService : IAuthenticationService
{
    #region Private Fields
    private readonly IConfiguration _config;
    #endregion

    #region Constructors
    public AuthenticationService(IConfiguration config)
    {
        _config = config;
    }
    #endregion

    #region Public Methods
    public string GenerateJwtToken(ApplicationUser user)
    {
        var claims = new Dictionary<string, object>
        {
            [ClaimTypes.NameIdentifier] = user.Id.ToString(),
            [ClaimTypes.Name] = user.UserName,
            [ClaimTypes.Email] = user.Email
        };

        var key = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(_config["Jwt:Key"]!)
        );

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Issuer = _config["Jwt:Issuer"],
            Audience = _config["Jwt:Audience"],
            Claims = claims,
            Expires = DateTime.UtcNow.AddMinutes(
                Convert.ToDouble(_config["Jwt:DurationInMinutes"])
            ),
            SigningCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256)
        };

        var handler = new JsonWebTokenHandler();
        return handler.CreateToken(tokenDescriptor);
    }

    #endregion
}
