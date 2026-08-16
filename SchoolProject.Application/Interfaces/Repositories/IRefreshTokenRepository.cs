using SchoolProject.Application.Interfaces.Bases;
using SchoolProject.Domain.Entities;
using SchoolProject.Domain.Entities.Identities;

namespace SchoolProject.Application.Interfaces.Repositories;

public interface IRefreshTokenRepository : IGenericRepositoryAsync<RefreshToken>
{
    public Task<RefreshToken?> GetByTokenAsync(string token);

    public (string RawToken, RefreshToken RefreshToken) GenerateRefreshToken(int userId, Guid? familyId = null);
    public Task RevokeAsync(RefreshToken refreshToken);
    public Task RevokeFamilyAsync(Guid familyId);
};

