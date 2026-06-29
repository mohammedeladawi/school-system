using SchoolProject.Data.Entities.Identities;

namespace SchoolProject.Service.Abstracts;

public interface IAuthenticationService
{
    string GenerateJwtToken(ApplicationUser user, List<string>? userRoles = null);
    (string RawToken, RefreshToken RefreshToken) GenerateRefreshToken(int userId, Guid? familyId = null);
    Task AddRefreshTokenAsync(RefreshToken refreshToken);

    Task<RefreshToken?> GetRefreshTokenByTokenHashAsync(string tokenHash);
    Task RevokeRefreshTokenAsync(RefreshToken refreshToken);
    Task RevokeRefreshTokenFamilyAsync(Guid familyId);
}
