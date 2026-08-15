using SchoolProject.Application.Interfaces.Bases;
using SchoolProject.Domain.Entities;

namespace SchoolProject.Application.Interfaces.Repositories;

public interface IPasswordResetCodeRepository : IGenericRepositoryAsync<PasswordResetCode>
{
    public string GeneratePasswordResetCode();

    public Task RevokeOldPasswordResetCodesAsync(int userId);

    public Task<PasswordResetCode?> GetByUserIdAndCode(int userId, string hashedCode);
}
