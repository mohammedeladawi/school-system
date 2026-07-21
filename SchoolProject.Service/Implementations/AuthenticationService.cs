using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;
using SchoolProject.Data.Entities.Identities;
using SchoolProject.Infrastructure.Abstracts;
using SchoolProject.Service.Abstracts;
using SchoolProject.Shared.Helpers;

namespace SchoolProject.Service.Implementations;

public class AuthenticationService : IAuthenticationService
{
    #region Private Fields
    private readonly IConfiguration _config;
    private readonly IRefreshTokenRepository _refreshTokenRepository;
    private readonly IAuthorizationService _authorizationService;
    #endregion

    #region Constructors
    public AuthenticationService(
        IConfiguration config,
        IRefreshTokenRepository refreshTokenRepository,
        IAuthorizationService authorizationService)
    {
        _config = config;
        _refreshTokenRepository = refreshTokenRepository;
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
        new Claim(ClaimTypes.Email, user.Email!)
    };

        var roles = await _authorizationService.GetUserRolesAsync(user.Id);

        foreach (var role in roles)
        {
            claims.Add(new Claim(ClaimTypes.Role, role));
        }

        var permissions = await _authorizationService.GetUserPermissionsAsync(user.Id);

        foreach (var permission in permissions)
        {
            claims.Add(new Claim("Permission", permission));
        }

        var key = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(_config["Jwt:Key"]!)
        );

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Issuer = _config["Jwt:Issuer"],
            Audience = _config["Jwt:Audience"],
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.UtcNow.AddMinutes(
                Convert.ToDouble(_config["Jwt:DurationInMinutes"])
            ),
            SigningCredentials = new SigningCredentials(
                key,
                SecurityAlgorithms.HmacSha256)
        };

        var handler = new JsonWebTokenHandler();

        return handler.CreateToken(tokenDescriptor);
    }

    public (string RawToken, RefreshToken RefreshToken) GenerateRefreshToken(int userId, Guid? familyId = null)
    {
        string rawToken = Guid.NewGuid().ToString();
        string tokenHash = Utils.Hash(rawToken);

        var refreshToken = new RefreshToken
        {
            Id = Guid.NewGuid(),
            UserId = userId,
            TokenHash = tokenHash,
            ExpiresAt = DateTime.UtcNow.AddMinutes(
                Convert.ToDouble(_config["Jwt:RefreshTokenInMinutes"])
            ),
            CreatedAt = DateTime.UtcNow,
            IsRevoked = false,
            FamilyId = familyId ?? Guid.NewGuid(),
        };

        return (rawToken, refreshToken);

    }

    public async Task AddRefreshTokenAsync(RefreshToken refreshToken)
    {
        await _refreshTokenRepository.AddAsync(refreshToken);
    }

    public Task<RefreshToken?> GetRefreshTokenByTokenHashAsync(string tokenHash)
    {
        return _refreshTokenRepository.GetByTokenHashAsync(tokenHash);
    }

    public Task RevokeRefreshTokenAsync(RefreshToken refreshToken)
    {
        refreshToken.IsRevoked = true;
        return _refreshTokenRepository.UpdateAsync(refreshToken);
    }

    public Task RevokeRefreshTokenFamilyAsync(Guid familyId)
    {
        return _refreshTokenRepository.RevokeTokenFamilyAsync(familyId);
    }

    #endregion
}
