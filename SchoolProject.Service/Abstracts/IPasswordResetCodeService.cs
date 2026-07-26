using SchoolProject.Data.Entities;
using SchoolProject.Data.Entities.Identities;

namespace SchoolProject.Service.Abstracts;

public interface IPasswordResetCodeService
{
    public string GeneratePasswordResetCode();

    public Task RevokeOldPasswordResetCodesAsync(int userId);

    public Task AddAsync(PasswordResetCode passwordResetCode);
}
