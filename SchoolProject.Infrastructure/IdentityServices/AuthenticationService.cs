using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;
using SchoolProject.Core.Interfaces.IdentityServices;
using SchoolProject.Core.Interfaces.Repositories;
using SchoolProject.Core.Interfaces.Services;
using SchoolProject.Data.Entities;
using SchoolProject.Data.Entities.Identities;
using SchoolProject.Infrastructure.Data;
using SchoolProject.Shared.Helpers;

namespace SchoolProject.Infrastructure.IdentityServices;

public class AuthenticationService : IAuthenticationService
{
    #region Private Fields
    private readonly IConfiguration _config;
    private readonly IAuthorizationService _authorizationService;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly AppDbContext _dbContext;
    private readonly IPasswordResetCodeRepository _passwordResetCodeRepository;
    private readonly ILogger<AuthenticationService> _logger;

    #endregion

    #region Constructors
    public AuthenticationService(
        IConfiguration config,
        IAuthorizationService authorizationService,
        UserManager<ApplicationUser> userManager,
        AppDbContext dbContext,
        IPasswordResetCodeRepository passwordResetCodeService,
        ILogger<AuthenticationService> logger)
    {
        _config = config;
        _authorizationService = authorizationService;
        _userManager = userManager;
        _dbContext = dbContext;
        _passwordResetCodeRepository = passwordResetCodeService;
        _logger = logger;
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

    public async Task<string> GenerateEncodedEmailConfirmationTokenAsync(ApplicationUser user)
    {
        var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
        var encodedToken = Utils.Encode(token);
        return encodedToken;
    }

    public async Task ConfirmEmailAsync(ApplicationUser user, string encodedToken)
    {
        string decodedToken = Utils.Decode(encodedToken);
        var confirmationResult = await _userManager.ConfirmEmailAsync(user, decodedToken);

        if (!confirmationResult.Succeeded)
            throw new Exception(string.Join(" ", confirmationResult.Errors.Select(e => e.Description)));
    }

    public async Task ResetPasswordAsync(ApplicationUser user, string code, string newPassword)
    {
        using var transaction = await _dbContext.Database.BeginTransactionAsync();

        try
        {
            var passwordResetCode = await _passwordResetCodeRepository.GetByUserIdAndCode(user.Id, code);
            if (passwordResetCode is null)
                throw new Exception("Invalid password reset code.");

            if (passwordResetCode.IsRevoked || passwordResetCode.ExpirationDate < DateTime.UtcNow)
                throw new Exception("Password reset code is invalid or expired.");

            var resetToken = await _userManager.GeneratePasswordResetTokenAsync(user);
            var result = await _userManager.ResetPasswordAsync(user, resetToken, newPassword);
            if (!result.Succeeded)
                throw new Exception(string.Join(" ", result.Errors.Select(e => e.Description)));

            await _passwordResetCodeRepository.RevokeOldPasswordResetCodesAsync(user.Id);

            transaction.Commit();
        }
        catch
        {
            transaction.Rollback();
            throw;
        }
    }


    #endregion
}
