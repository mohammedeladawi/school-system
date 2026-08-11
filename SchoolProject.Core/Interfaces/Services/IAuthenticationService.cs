using SchoolProject.Data.Entities.Identities;

namespace SchoolProject.Core.Interfaces.Services;

public interface IAuthenticationService
{
    public Task<string> GenerateJwtTokenAsync(ApplicationUser user);
    public (string RawToken, RefreshToken RefreshToken) GenerateRefreshToken(int userId, Guid? familyId = null);
    public Task AddRefreshTokenAsync(RefreshToken refreshToken);
    public Task<RefreshToken?> GetRefreshTokenByTokenHashAsync(string tokenHash);
    public Task RevokeRefreshTokenAsync(RefreshToken refreshToken);
    public Task RevokeRefreshTokenFamilyAsync(Guid familyId);
    public Task<string> RegisterAndSendConfirmationEmailAsync(ApplicationUser user, string password, string confirmationUrlTemplate);
    public Task ConfirmEmailAsync(ApplicationUser user, string token);
    public Task GenerateAndSendPasswordResetCodeAsync(ApplicationUser user);
    public Task ResetPasswordAsync(ApplicationUser user, string code, string newPassword);

}
