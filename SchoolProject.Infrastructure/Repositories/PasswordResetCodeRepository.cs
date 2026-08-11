using Microsoft.EntityFrameworkCore;
using SchoolProject.Core.Interfaces.Repositories;
using SchoolProject.Data.Entities;
using SchoolProject.Infrastructure.Data;
using SchoolProject.Infrastructure.InfrastructureBases;
using SchoolProject.Shared.Helpers;

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

    public string GeneratePasswordResetCode()
    {
        string code = Random.Shared.Next(100000, 999999).ToString();
        return code;
    }

    public async Task<PasswordResetCode?> GetByUserIdAndHashedCode(int userId, string hashedCode)
    {
        // ======= Todo: move hashing to application layer ========
        hashedCode = Utils.Hash(hashedCode);

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