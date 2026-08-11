using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;
using SchoolProject.Core.Interfaces.Identities;
using SchoolProject.Core.Interfaces.Repositories;
using SchoolProject.Core.Interfaces.Services;
using SchoolProject.Data.Entities;
using SchoolProject.Data.Entities.Identities;
using SchoolProject.Infrastructure.Data;
using SchoolProject.Shared.Helpers;

namespace SchoolProject.Infrastructure.Services;

public class AuthenticationService : IAuthenticationService
{
    #region Private Fields
    private readonly IConfiguration _config;
    private readonly IRefreshTokenRepository _refreshTokenRepository;
    private readonly IAuthorizationService _authorizationService;
    private readonly IEmailService _emailService;
    private readonly IApplicationUserRepository _applicationUserRepository;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly AppDbContext _dbContext;
    private readonly IPasswordResetCodeRepository _passwordResetCodeRepository;
    private readonly ILogger<AuthenticationService> _logger;

    #endregion

    #region Constructors
    public AuthenticationService(
        IConfiguration config,
        IRefreshTokenRepository refreshTokenRepository,
        IAuthorizationService authorizationService,
        IEmailService emailService,
        IApplicationUserRepository ApplicationUserRepositories,
        UserManager<ApplicationUser> userManager,
        AppDbContext dbContext,
        IPasswordResetCodeRepository passwordResetCodeService,
        ILogger<AuthenticationService> logger)
    {
        _config = config;
        _refreshTokenRepository = refreshTokenRepository;
        _authorizationService = authorizationService;
        _emailService = emailService;
        _applicationUserRepository = ApplicationUserRepositories;
        _userManager = userManager;
        _dbContext = dbContext;
        _passwordResetCodeRepository = passwordResetCodeService;
        _logger = logger;
    }

    #endregion


    #region Private Methods
    private async Task SendConfirmationEmailAsync(ApplicationUser user, string token, string confirmationUrlTemplate)
    {
        var confirmationUrl = string.Format(confirmationUrlTemplate, user.Id, token);
        var emailSubject = "Confirm your email";
        var emailBody = $"""
                <h1>Welcome {user.UserName}</h1>

                <p>Thank you for registering.</p>

                <p>Please confirm your email address by clicking the link below:</p>

                <a href="{confirmationUrl}">
                    Confirm Email
                </a>

                <p>If you did not create this account, ignore this email.</p>
                """;

        await _emailService.SendEmailAsync(
            user.Email,
            emailBody,
            emailSubject);
    }

    private async Task<string> GenerateEncodedEmailConfirmationTokenAsync(ApplicationUser user)
    {
        var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
        var encodedToken = Utils.Encode(token);
        return encodedToken;
    }

    private async Task SendPasswordResetCodeEmailAsync(string userEmail, string rawCode)
    {
        var subject = "Password Reset Code";
        var body = $"Your password reset code is: {rawCode}. It will expire in 15 minutes.";
        await _emailService.SendEmailAsync(userEmail, body, subject);
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

    public async Task<string> RegisterAndSendConfirmationEmailAsync(ApplicationUser user, string password, string confirmationUrlTemplate)
    {
        using var transaction = await _dbContext.Database.BeginTransactionAsync();

        try
        {
            await _applicationUserRepository.AddAsync(user, password);
            var token = await GenerateEncodedEmailConfirmationTokenAsync(user);

            await SendConfirmationEmailAsync(user, token, confirmationUrlTemplate);

            transaction.Commit();
            return token;
        }
        catch (Exception)
        {
            transaction.Rollback();
            throw;
        }
    }

    public async Task ConfirmEmailAsync(ApplicationUser user, string encodedToken)
    {
        string decodedToken = Utils.Decode(encodedToken);
        var confirmationResult = await _userManager.ConfirmEmailAsync(user, decodedToken);

        if (!confirmationResult.Succeeded)
            throw new Exception(string.Join(" ", confirmationResult.Errors.Select(e => e.Description)));
    }

    public async Task GenerateAndSendPasswordResetCodeAsync(ApplicationUser user)
    {
        using var transaction = await _dbContext.Database.BeginTransactionAsync();
        try
        {
            await _passwordResetCodeRepository.RevokeOldPasswordResetCodesAsync(user.Id);
            var rawCode = _passwordResetCodeRepository.GeneratePasswordResetCode();

            var passwordResetCode = new PasswordResetCode
            {
                UserId = user.Id,
                HashedCode = Utils.Hash(rawCode),
                ExpirationDate = DateTime.UtcNow.AddMinutes(15)
            };

            await _passwordResetCodeRepository.AddAsync(passwordResetCode);
            await SendPasswordResetCodeEmailAsync(user.Email, rawCode);

            await transaction.CommitAsync();
        }
        catch
        {
            await transaction.RollbackAsync();
            _logger.LogError("Failed to generate and send password reset code for user {UserId}", user.Id);
            throw;
        }

    }

    public async Task ResetPasswordAsync(ApplicationUser user, string code, string newPassword)
    {
        using var transaction = await _dbContext.Database.BeginTransactionAsync();

        try
        {
            var passwordResetCode = await _passwordResetCodeRepository.GetByUserIdAndHashedCode(user.Id, code);
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
