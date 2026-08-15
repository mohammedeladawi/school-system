using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Localization;
using SchoolProject.Application.Bases;
using SchoolProject.Application.Interfaces.Bases;
using SchoolProject.Application.Interfaces.IdentityServices;
using SchoolProject.Application.Interfaces.Repositories;
using SchoolProject.Shared.AppMetaData;
using SchoolProject.Shared.Helpers;
using SchoolProject.Application.Resources;

namespace SchoolProject.Application.Features.Authentication.Commands.VerifyResetCode
{
    public class VerifyResetCodeHandler : ResponseHandler, IRequestHandler<VerifyResetCodeCommand, Response<ResetPasswordUrlResponse>>
    {
        #region Private Fields
        private readonly IUserManager _userManager;
        private readonly IPasswordResetCodeRepository _passwordResetCodeRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;
        #endregion

        #region Constructors
        public VerifyResetCodeHandler(
            IUserManager userManager,
            IMapper mapper,
            IStringLocalizer<SharedResource> localizer,
            IPasswordResetCodeRepository passwordResetCodeRepository,
            IHttpContextAccessor httpContextAccessor)
            : base(localizer, mapper)
        {
            _userManager = userManager;
            _passwordResetCodeRepository = passwordResetCodeRepository;
            _httpContextAccessor = httpContextAccessor;
        }
        #endregion

        #region Private Methods

        private string GetResetPasswordUrl(string encodedUserId, string encodedCode)
        {
            var request = _httpContextAccessor.HttpContext!.Request;
            string scheme = request.Scheme;
            string host = request.Host.ToString();
            string path = Router.Authentication.ResetPassword;

            return $"{scheme}://{host}/{path}?encodedUserId={encodedUserId}&encodedCode={encodedCode}";
        }

        #endregion

        #region Public Methods
        public async Task<Response<ResetPasswordUrlResponse>> Handle(VerifyResetCodeCommand request, CancellationToken cancellationToken)
        {
            var user = await _userManager.GetByEmailAsync(request.Email);
            var passwordResetCode = await _passwordResetCodeRepository.GetByUserIdAndCode(user!.Id, request.Code);

            if (passwordResetCode is null)
                return NotFound<ResetPasswordUrlResponse>(_localizer[SharedResourceKeys.InvalidOTP]);

            if (passwordResetCode.IsRevoked || passwordResetCode.ExpirationDate < DateTime.UtcNow)
                return BadRequest<ResetPasswordUrlResponse>(_localizer[SharedResourceKeys.InvalidOTP]);

            string encodedUserId = Utils.Encode(user.Id.ToString());
            string encodedCode = Utils.Encode(request.Code);

            var resetPasswordUrl = GetResetPasswordUrl(encodedUserId, encodedCode);

            return Success(new ResetPasswordUrlResponse(resetPasswordUrl));
        }
        #endregion
    }
}
