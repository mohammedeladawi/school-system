using SchoolProject.Data.Entities;
using SchoolProject.Data.Entities.Identities;
using SchoolProject.Infrastructure.InfrastructureBases;

namespace SchoolProject.Infrastructure.Abstracts;

public interface IRefreshTokenRepository : IGenericRepositoryAsync<RefreshToken>
{
    public Task<RefreshToken?> GetByTokenHashAsync(string tokenHash);
    public Task RevokeTokenFamilyAsync(Guid familyId);
};

