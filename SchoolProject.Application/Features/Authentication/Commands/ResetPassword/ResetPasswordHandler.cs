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
        private readonly IUserManager _userManager;
        private readonly IPasswordResetCodeRepository _passwordResetCodeRepository;
        private readonly IUnitOfWork _unitOfWork;
        #endregion

        #region Constructors
        public ResetPasswordHandler(
            IUserManager userManager,
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

        #endregion

        #region Public Methods
        public async Task<Response<string>> Handle(ResetPasswordCommand request, CancellationToken cancellationToken)
        {
            int userId = int.Parse(Utils.Decode(request.EncodedUserId));
            var user = await _userManager.GetByIdAsync(userId);

            await _unitOfWork.BeginTransactionAsync();
            try
            {
                string code = Utils.Decode(request.EncodedCode);
                await ValidatePasswordResetCodeAsync(user!.Id, code);
                await _userManager.ChangePasswordAsync(user!, request.NewPassword);
                await _passwordResetCodeRepository.RevokeOldPasswordResetCodesAsync(user.Id);

                await _unitOfWork.CommitAsync();
            }
            catch
            {
                await _unitOfWork.RollbackAsync();
                throw;
            }

            return Success<string>(_localizer[SharedResourceKeys.ResetPasswordSuccessfully]);
        }
        #endregion
    }
}
