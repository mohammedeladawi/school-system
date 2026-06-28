using SchoolProject.Data.Entities;
using SchoolProject.Data.Entities.Identities;
using SchoolProject.Infrastructure.InfrastructureBases;

namespace SchoolProject.Infrastructure.Abstracts;

public interface IRefreshTokenRepository : IGenericRepositoryAsync<RefreshToken>
{
    // No additional methods beyond inherited generic methods.
};

