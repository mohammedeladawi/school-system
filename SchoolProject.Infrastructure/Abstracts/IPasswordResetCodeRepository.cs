using SchoolProject.Data.Entities;
using SchoolProject.Infrastructure.InfrastructureBases;

namespace SchoolProject.Infrastructure.Abstracts;

public interface IPasswordResetCodeRepository : IGenericRepositoryAsync<PasswordResetCode>
{
    Task RevokeOldPasswordResetCodesAsync(int userId);
    Task<PasswordResetCode?> GetByUserIdAndHashedCode(int userId, string hashedCode);
}

