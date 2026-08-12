using Microsoft.EntityFrameworkCore;
using SchoolProject.Core.Interfaces.Repositories;
using SchoolProject.Data.Entities;
using SchoolProject.Data.Entities.Identities;
using SchoolProject.Infrastructure.Data;
using SchoolProject.Infrastructure.InfrastructureBases;

namespace SchoolProject.Infrastructure.Repositories;

public class RefreshTokenRepository :
    GenericRepositoryAsync<RefreshToken>,
    IRefreshTokenRepository
{
    #region Private Fields
    private readonly DbSet<RefreshToken> _refreshTokens;
    #endregion

    #region Constructors
    public RefreshTokenRepository(AppDbContext context) : base(context)
    {
        _refreshTokens = context.RefreshTokens;
    }

    #endregion

    #region Public Methods

    public Task<RefreshToken?> GetByTokenHashAsync(string tokenHash)
    {
        return _refreshTokens.Include(rt => rt.User)
                             .FirstOrDefaultAsync(rt => rt.TokenHash == tokenHash);
    }

    public Task RevokeTokenFamilyAsync(Guid familyId)
    {
        return _refreshTokens.Where(rt => rt.FamilyId == familyId && !rt.IsRevoked)
                    .ExecuteUpdateAsync(rt => rt.SetProperty(r => r.IsRevoked, true));

    }

    #endregion
}
