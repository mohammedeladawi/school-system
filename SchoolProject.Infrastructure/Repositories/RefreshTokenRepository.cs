using Microsoft.EntityFrameworkCore;
using SchoolProject.Data.Entities;
using SchoolProject.Data.Entities.Identities;
using SchoolProject.Infrastructure.Abstracts;
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
        _refreshTokens = context.Set<RefreshToken>();
    }
    #endregion

    #region Public Methods
    // No public methods beyond inherited generic methods.
    #endregion
}
