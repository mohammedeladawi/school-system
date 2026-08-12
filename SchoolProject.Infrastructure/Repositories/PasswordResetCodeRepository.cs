using Microsoft.EntityFrameworkCore;
using SchoolProject.Core.Interfaces.Repositories;
using SchoolProject.Data.Entities;
using SchoolProject.Infrastructure.Data;
using SchoolProject.Infrastructure.Bases;
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
        _passwordResetCodes = context.PasswordResetCodes;
    }


    #region Public Methods
    public string GeneratePasswordResetCode()
    {
        string code = Random.Shared.Next(100000, 999999).ToString();
        return code;
    }

    public async Task<PasswordResetCode?> GetByUserIdAndCode(int userId, string code)
    {
        var hashedCode = Utils.Hash(code);

        return await _passwordResetCodes
            .AsNoTracking()
            .FirstOrDefaultAsync(c => c.UserId == userId && c.HashedCode == hashedCode);
    }
    #endregion

    public async Task RevokeOldPasswordResetCodesAsync(int userId)
    {
        await _passwordResetCodes.Where(c => c.UserId == userId && !c.IsRevoked)
                                 .ExecuteUpdateAsync(c => c.SetProperty(x => x.IsRevoked, true));
    }

    #endregion
}