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
    private async Task SendPasswordResetCodeEmailAsync(string userEmail, string rawCode)
    {
        var subject = "Password Reset Code";
        var body = $"Your password reset code is: {rawCode}. It will expire in 15 minutes.";
        await _emailService.SendEmailAsync(userEmail, body, subject);
    }

    #endregion

    #region Public Methods
    public async Task AddAsync(PasswordResetCode passwordResetCode)
    {
        await _passwordResetCodeRepository.AddAsync(passwordResetCode);

    }

    public async Task GenerateAndSendPasswordResetCodeAsync(ApplicationUser user)
    {
        var transaction = await _dbContext.Database.BeginTransactionAsync();
        try
        {
            await RevokeOldPasswordResetCodesAsync(user.Id);
            var rawCode = GeneratePasswordResetCode();

            var passwordResetCode = new PasswordResetCode
            {
                UserId = user.Id,
                HashedCode = Utils.Hash(rawCode),
                ExpirationDate = DateTime.UtcNow.AddMinutes(15)
            };

            await AddAsync(passwordResetCode);
            await SendPasswordResetCodeEmailAsync(user.Email, rawCode);

            await transaction.CommitAsync();
        }
        catch
        {
            await transaction.RollbackAsync();
            throw;
        }

    }

    public string GeneratePasswordResetCode()
    {
        string code = Random.Shared.Next(100000, 999999).ToString();
        return code;
    }

    public async Task RevokeOldPasswordResetCodesAsync(int userId)
    {
        await _passwordResetCodeRepository.RevokeOldPasswordResetCodesAsync(userId);
    }

    #endregion
}