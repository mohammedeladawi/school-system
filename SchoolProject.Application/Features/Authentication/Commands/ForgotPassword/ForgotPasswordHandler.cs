using AutoMapper;
using MediatR;
using Microsoft.Extensions.Localization;
using SchoolProject.Application.Bases;
using SchoolProject.Application.Interfaces.Bases;
using SchoolProject.Application.Interfaces.IdentityServices;
using SchoolProject.Application.Interfaces.Repositories;
using SchoolProject.Application.Interfaces.Services;
using SchoolProject.Application.Helpers;
using SchoolProject.Application.Resources;

namespace SchoolProject.Application.Features.Authentication.Commands.ForgotPassword
{
    public class ForgotPasswordHandler : ResponseHandler, IRequestHandler<ForgotPasswordCommand, Response<string>>
    {
        #region Private Fields
        private readonly IUserManager _userManager;
        private readonly IPasswordResetCodeRepository _passwordResetCodeRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IEmailService _emailService;
        #endregion

        #region Constructors
        public ForgotPasswordHandler(
            IUserManager userManager,
            IMapper mapper,
            IStringLocalizer<SharedResource> localizer,
            IPasswordResetCodeRepository passwordResetCodeRepository,
            IEmailService emailService,
            IUnitOfWork unitOfWork)
            : base(localizer, mapper)
        {
            _userManager = userManager;
            _passwordResetCodeRepository = passwordResetCodeRepository;
            _emailService = emailService;
            _unitOfWork = unitOfWork;
        }
        #endregion

        #region Private Methods
        private (string Subject, string Body) GetComposedEmailContent(string rawCode)
        {
            string subject = "Password Reset Code";
            string body = $"Your password reset code is: {rawCode}. It will expire in 15 minutes.";

            return (subject, body);
        }
        #endregion

        #region Public Methods
        public async Task<Response<string>> Handle(ForgotPasswordCommand request, CancellationToken cancellationToken)
        {
            var user = await _userManager.GetByEmailAsync(request.Email);
            if (!user!.EmailConfirmed) return BadRequest<string>(_localizer[SharedResourceKeys.EmailNotConfirmed]);

            await _unitOfWork.BeginTransactionAsync();
            try
            {
                // revoke any existing password reset codes for the user before generating a new one
                await _passwordResetCodeRepository.RevokeOldPasswordResetCodesAsync(user.Id);

                // generate a new password reset code and save it to the database
                var rawCode = _passwordResetCodeRepository.GeneratePasswordResetCode();
                var passwordResetCode = new Domain.Entities.PasswordResetCode
                {
                    UserId = user.Id,
                    HashedCode = Utils.Hash(rawCode),
                    ExpirationDate = DateTime.UtcNow.AddMinutes(15)
                };
                await _passwordResetCodeRepository.AddAsync(passwordResetCode);

                // send the password reset code to the user's email
                (string subject, string body) = GetComposedEmailContent(rawCode);
                await _emailService.SendEmailAsync(user.Email!, body, subject);

                await _unitOfWork.CommitAsync();
            }
            catch
            {
                await _unitOfWork.RollbackAsync();
                throw;
            }

            return Success<string>(_localizer[SharedResourceKeys.PasswordResetCodeSentSuccessfully]);
        }
        #endregion
    }
}
