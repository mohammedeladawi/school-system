using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Localization;
using SchoolProject.Application.Bases;
using SchoolProject.Application.Interfaces.Bases;
using SchoolProject.Application.Interfaces.IdentityServices;
using SchoolProject.Application.Interfaces.Repositories;
using SchoolProject.Application.Helpers;
using SchoolProject.Application.Resources;

namespace SchoolProject.Application.Features.Authentication.Commands.ResetPassword
{
    public class ResetPasswordHandler : ResponseHandler, IRequestHandler<ResetPasswordCommand, Response<string>>
    {
        #region Private Fields
        private readonly UserManager<Domain.Entities.Identities.ApplicationUser> _userManager;
        private readonly IPasswordResetCodeRepository _passwordResetCodeRepository;
        private readonly IUnitOfWork _unitOfWork;
        #endregion

        #region Constructors
        public ResetPasswordHandler(
            UserManager<Domain.Entities.Identities.ApplicationUser> userManager,
            IMapper mapper,
            IStringLocalizer<SharedResource> localizer,
            IPasswordResetCodeRepository passwordResetCodeRepository,
            IUnitOfWork unitOfWork)
            : base(localizer, mapper)
        {
            _userManager = userManager;
            _passwordResetCodeRepository = passwordResetCodeRepository;
            _unitOfWork = unitOfWork;
        }
        #endregion

        #region Private Methods
        private async Task ValidatePasswordResetCodeAsync(int userId, string code)
        {
            var passwordResetCode = await _passwordResetCodeRepository.GetByUserIdAndCode(userId, code);
            if (passwordResetCode is null)
                throw new Exception("Invalid password reset code.");

            if (passwordResetCode.IsRevoked || passwordResetCode.ExpirationDate < DateTime.UtcNow)
                throw new Exception("Password reset code is invalid or expired.");
        }

        private async Task ChangePasswordAsync(Domain.Entities.Identities.ApplicationUser user, string newPassword)
        {
            string resetToken = await _userManager.GeneratePasswordResetTokenAsync(user!);
            var result = await _userManager.ResetPasswordAsync(user!, resetToken, newPassword);
            if (!result.Succeeded)
                throw new Exception(string.Join(" ", result.Errors.Select(e => e.Description)));
        }

        #endregion

        #region Public Methods
        public async Task<Response<string>> Handle(ResetPasswordCommand request, CancellationToken cancellationToken)
        {
            string userId = Utils.Decode(request.EncodedUserId);
            var user = await _userManager.FindByIdAsync(userId);

            using (await _unitOfWork.BeginTransactionAsync())
            {
                try
                {
                    string code = Utils.Decode(request.EncodedCode);
                    await ValidatePasswordResetCodeAsync(user!.Id, code);
                    await ChangePasswordAsync(user!, request.NewPassword);
                    await _passwordResetCodeRepository.RevokeOldPasswordResetCodesAsync(user.Id);

                    await _unitOfWork.CommitAsync();
                }
                catch
                {
                    await _unitOfWork.RollbackAsync();
                    throw;
                }
            }

            return Success<string>(_localizer[SharedResourceKeys.ResetPasswordSuccessfully]);
        }
        #endregion
    }
}
