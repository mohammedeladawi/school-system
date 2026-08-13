using SchoolProject.Core.Interfaces.Bases;
using SchoolProject.Data.Entities;
using SchoolProject.Data.Entities.Identities;

namespace SchoolProject.Core.Interfaces.Repositories;

public interface IRefreshTokenRepository : IGenericRepositoryAsync<RefreshToken>
{
    public Task<RefreshToken?> GetByTokenHashAsync(string tokenHash);

    public (string RawToken, RefreshToken RefreshToken) GenerateRefreshToken(int userId, Guid? familyId = null);
    public Task RevokeAsync(RefreshToken refreshToken);
    public Task RevokeFamilyAsync(Guid familyId);
};

