using SchoolProject.Data.Entities;
using SchoolProject.Data.Entities.Identities;
using SchoolProject.Infrastructure.Abstracts;
using SchoolProject.Infrastructure.Data;
using SchoolProject.Service.Abstracts;
using SchoolProject.Shared.Helpers;

namespace SchoolProject.Service.Implementations;

public class PasswordResetCodeService : IPasswordResetCodeService
{
    #region Private Fields
    private readonly IPasswordResetCodeRepository _passwordResetCodeRepository;
    private readonly IEmailService _emailService;
    private readonly AppDbContext _dbContext;
    #endregion

    #region Constructors
    public PasswordResetCodeService(
        IPasswordResetCodeRepository passwordResetCodeRepository,
        IEmailService emailService,
        AppDbContext dbContext)
    {
        _passwordResetCodeRepository = passwordResetCodeRepository;
        _emailService = emailService;
        _dbContext = dbContext;
    }

    #endregion


    #region Private Methods

    #endregion

    #region Public Methods
    public async Task AddAsync(PasswordResetCode passwordResetCode)
    {
        await _passwordResetCodeRepository.AddAsync(passwordResetCode);

    }


    public string GeneratePasswordResetCode()
    {
        string code = Random.Shared.Next(100000, 999999).ToString();
        return code;
    }

    public async Task<PasswordResetCode?> GetByUserIdAndCodeAsync(int userId, string code)
    {
        string hashedCode = Utils.Hash(code);
        var passwordResetCode = await _passwordResetCodeRepository.GetByUserIdAndHashedCode(userId, hashedCode);
        return passwordResetCode;
    }

    public async Task RevokeOldPasswordResetCodesAsync(int userId)
    {
        await _passwordResetCodeRepository.RevokeOldPasswordResetCodesAsync(userId);
    }

    #endregion
}