using SchoolProject.Core.Interfaces.Bases;
using SchoolProject.Data.Entities;

namespace SchoolProject.Core.Interfaces.Repositories;

public interface IPasswordResetCodeRepository : IGenericRepositoryAsync<PasswordResetCode>
{
    public string GeneratePasswordResetCode();

    public Task RevokeOldPasswordResetCodesAsync(int userId);

    public Task<PasswordResetCode?> GetByUserIdAndHashedCode(int userId, string hashedCode);
}
