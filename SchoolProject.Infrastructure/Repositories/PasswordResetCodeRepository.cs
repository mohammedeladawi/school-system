using Microsoft.EntityFrameworkCore;
using SchoolProject.Data.Entities;
using SchoolProject.Infrastructure.Abstracts;
using SchoolProject.Infrastructure.Data;
using SchoolProject.Infrastructure.InfrastructureBases;

namespace SchoolProject.Infrastructure.Repositories;

public class PasswordResetCodeRepository :
    GenericRepositoryAsync<PasswordResetCode>,
    IPasswordResetCodeRepository
{
    #region Private Fields
    private readonly DbSet<PasswordResetCode> _passwordResetCodes;
    #endregion

    #region Constructors
    public PasswordResetCodeRepository(AppDbContext context) : base(context)
    {
        _passwordResetCodes = context.Set<PasswordResetCode>();
    }

    public async Task<PasswordResetCode?> GetByUserIdAndHashedCode(int userId, string hashedCode)
    {
        return await _passwordResetCodes
            .AsNoTracking()
            .FirstOrDefaultAsync(c => c.UserId == userId && c.HashedCode == hashedCode);
    }
    #endregion

    #region Public Methods
    public async Task RevokeOldPasswordResetCodesAsync(int userId)
    {
        await _passwordResetCodes.Where(c => c.UserId == userId && !c.IsRevoked)
                                 .ExecuteUpdateAsync(c => c.SetProperty(x => x.IsRevoked, true));
    }
    #endregion
}