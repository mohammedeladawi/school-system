using Microsoft.EntityFrameworkCore;
using SchoolProject.Core.Interfaces.Repositories;
using SchoolProject.Data.Entities;
using SchoolProject.Data.Entities.Identities;
using SchoolProject.Infrastructure.Data;
using SchoolProject.Infrastructure.Bases;
using SchoolProject.Shared.Helpers;
using Microsoft.Extensions.Configuration;

namespace SchoolProject.Infrastructure.Repositories;

public class RefreshTokenRepository :
    GenericRepositoryAsync<RefreshToken>,
    IRefreshTokenRepository
{
    #region Private Fields
    private readonly DbSet<RefreshToken> _refreshTokens;
    private readonly IConfiguration _config;
    #endregion

    #region Constructors
    public RefreshTokenRepository(AppDbContext context, IConfiguration config) : base(context)
    {
        _refreshTokens = context.RefreshTokens;
        _config = config;
    }

    #endregion

    #region Public Methods

    public Task<RefreshToken?> GetByTokenHashAsync(string tokenHash)
    {
        return _refreshTokens.Include(rt => rt.User)
                             .FirstOrDefaultAsync(rt => rt.TokenHash == tokenHash);
    }

    public Task RevokeFamilyAsync(Guid familyId)
    {
        return _refreshTokens.Where(rt => rt.FamilyId == familyId && !rt.IsRevoked)
                    .ExecuteUpdateAsync(rt => rt.SetProperty(r => r.IsRevoked, true));
    }

    public Task RevokeAsync(RefreshToken refreshToken)
    {
        refreshToken.IsRevoked = true;
        return UpdateAsync(refreshToken);
    }

    public (string RawToken, RefreshToken RefreshToken) GenerateRefreshToken(int userId, Guid? familyId = null)
    {
        string rawToken = Guid.NewGuid().ToString();
        string tokenHash = Utils.Hash(rawToken);

        var refreshToken = new RefreshToken
        {
            Id = Guid.NewGuid(),
            UserId = userId,
            TokenHash = tokenHash,
            ExpiresAt = DateTime.UtcNow.AddMinutes(
                Convert.ToDouble(_config["Jwt:RefreshTokenInMinutes"])
            ),
            CreatedAt = DateTime.UtcNow,
            IsRevoked = false,
            FamilyId = familyId ?? Guid.NewGuid(),
        };

        return (rawToken, refreshToken);

    }

    #endregion
}
