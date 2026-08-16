using Microsoft.EntityFrameworkCore;
using SchoolProject.Application.Interfaces.Repositories;
using SchoolProject.Domain.Entities;
using SchoolProject.Domain.Entities.Identities;
using SchoolProject.Infrastructure.Data;
using SchoolProject.Infrastructure.Bases;
using SchoolProject.Application.Helpers;
using Microsoft.Extensions.Configuration;
using SchoolProject.Application.Helpers.ConfigBinders;
using Microsoft.Extensions.Options;

namespace SchoolProject.Infrastructure.Repositories;

public class RefreshTokenRepository :
    GenericRepositoryAsync<RefreshToken>,
    IRefreshTokenRepository
{
    #region Private Fields
    private readonly DbSet<RefreshToken> _refreshTokens;
    private readonly JwtSettings _jwtSettings;
    #endregion

    #region Constructors
    public RefreshTokenRepository(
        AppDbContext context,
        IOptions<JwtSettings> jwtSettings) : base(context)
    {
        _refreshTokens = context.RefreshTokens;
        _jwtSettings = jwtSettings.Value;
    }

    #endregion

    #region Public Methods

    public Task<RefreshToken?> GetByTokenAsync(string token)

    {
        string tokenHash = Utils.Hash(token);
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
                Convert.ToDouble(_jwtSettings.RefreshTokenDurationInMinutes)
            ),
            CreatedAt = DateTime.UtcNow,
            IsRevoked = false,
            FamilyId = familyId ?? Guid.NewGuid(),
        };

        return (rawToken, refreshToken);

    }

    #endregion
}
