using System.Security;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Localization;
using Org.BouncyCastle.Math.EC.Rfc7748;
using SchoolProject.Core.Bases;
using SchoolProject.Core.Features.Authentication.Commands.Models;
using SchoolProject.Core.Features.Authentication.Commands.Responses;
using SchoolProject.Core.Interfaces.Bases;
using SchoolProject.Core.Interfaces.IdentityServices;
using SchoolProject.Core.Interfaces.Repositories;
using SchoolProject.Core.Interfaces.Services;
using SchoolProject.Shared.AppMetaData;
using SchoolProject.Shared.Helpers;
using SchoolProject.Shared.Resources;

namespace SchoolProject.Core.Features.Authentication.Commands.Handlers
{
    public class AuthenticationCommandHandler :
        ResponseHandler,
        IRequestHandler<LoginCommand, Response<AuthResponse>>,
        IRequestHandler<RefreshTokenCommand, Response<AuthResponse>>,
        IRequestHandler<LogoutCommand, Response<string>>,
        IRequestHandler<ConfirmEmailCommand, Response<string>>,
        IRequestHandler<ForgotPasswordCommand, Response<string>>,
        IRequestHandler<RegisterCommand, Response<string>>,
        IRequestHandler<VerifyResetCodeCommand, Response<ResetPasswordUrlResponse>>,
        IRequestHandler<ResetPasswordCommand, Response<string>>

    {
        #region Private Fields
        private readonly IAuthenticationService _authenticationService;
        private readonly IUserManager _userManager;
        private readonly IPasswordResetCodeRepository _passwordResetCodeRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IEmailService _emailService;
        private readonly IRefreshTokenRepository _refreshTokenRepository;

        #endregion

        #region Private Methods
        private async Task SendConfirmationEmailAsync(
            Data.Entities.Identities.ApplicationUser user,
            string token,
            string confirmationUrlTemplate)
        {
            var confirmationUrl = string.Format(confirmationUrlTemplate, user.Id, token);
            var emailSubject = "Confirm your email";
            var emailBody = $"""
                <h1>Welcome {user.UserName}</h1>

                <p>Thank you for registering.</p>

                <p>Please confirm your email address by clicking the link below:</p>

                <a href="{confirmationUrl}">
                    Confirm Email
                </a>

                <p>If you did not create this account, ignore this email.</p>
                """;

            await _emailService.SendEmailAsync(
                user.Email,
                emailBody,
                emailSubject);
        }

        private async Task SendPasswordResetCodeEmailAsync(string userEmail, string rawCode)
        {
            var subject = "Password Reset Code";
            var body = $"Your password reset code is: {rawCode}. It will expire in 15 minutes.";
            await _emailService.SendEmailAsync(userEmail, body, subject);
        }

        #endregion

        #region Constructors
        public AuthenticationCommandHandler(
            IUserManager userManager,
            IAuthenticationService authenticationService,
            IMapper mapper,
            IStringLocalizer<SharedResource> localizer,
            IHttpContextAccessor httpContextAccessor,
            IPasswordResetCodeRepository passwordResetCodeRepIPasswordResetCodeRepository,
            IEmailService emailService,
            IRefreshTokenRepository refreshTokenRepository,
            IUnitOfWork unitOfWork)
            : base(localizer, mapper)
        {
            _userManager = userManager;
            _authenticationService = authenticationService;
            _httpContextAccessor = httpContextAccessor;
            _passwordResetCodeRepository = passwordResetCodeRepIPasswordResetCodeRepository;
            _emailService = emailService;
            _refreshTokenRepository = refreshTokenRepository;
            _unitOfWork = unitOfWork;

        }
        #endregion

        #region Public Methods
        public async Task<Response<string>> Handle(RegisterCommand request, CancellationToken cancellationToken)
        {
            var user = _mapper.Map<Data.Entities.Identities.ApplicationUser>(request);

            var confirmationUrlTemplate = $"{_httpContextAccessor!.HttpContext!.Request.Scheme}://{_httpContextAccessor!.HttpContext!.Request.Host}/{Router.Authentication.ConfirmEmail}?userId={{0}}&token={{1}}";

            using (await _unitOfWork.BeginTransactionAsync())
            {
                try
                {
                    await _userManager.AddAsync(user, request.Password);
                    var token = await _authenticationService.GenerateEncodedEmailConfirmationTokenAsync(user);
                    await SendConfirmationEmailAsync(user, token, confirmationUrlTemplate);

                    await _unitOfWork.CommitAsync();
                    await _unitOfWork.SaveChangesAsync();
                }
                catch (Exception)
                {
                    await _unitOfWork.RollbackAsync();
                    throw;
                }

                return Created<string>(_localizer[SharedResourceKeys.AddedSuccessfully]);
            }
        }

        public async Task<Response<AuthResponse>> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            var user = await _userManager.GetByUserNameAndPasswordAsync(request.UserName, request.Password);
            if (user is null)
                return BadRequest<AuthResponse>(_localizer[SharedResourceKeys.InvalidUserNameOrPassword]);

            if (!user.EmailConfirmed)
                return Unauthorized<AuthResponse>(_localizer[SharedResourceKeys.EmailDoesNotConfirmed]);

            var accessToken = await _authenticationService.GenerateJwtTokenAsync(user);
            var (rawToken, refreshToken) = _refreshTokenRepository.GenerateRefreshToken(user.Id);
            await _refreshTokenRepository.AddAsync(refreshToken);
            await _unitOfWork.SaveChangesAsync();

            var authResponse = new AuthResponse
            {
                JwtToken = accessToken,
                RefreshToken = rawToken
            };

            return Success(authResponse);
        }

        public async Task<Response<AuthResponse>> Handle(RefreshTokenCommand request, CancellationToken cancellationToken)
        {
            string tokenHash = Utils.Hash(request.RefreshToken);
            var refreshToken = await _refreshTokenRepository.GetByTokenHashAsync(tokenHash);
            if (refreshToken is null)
                return Unauthorized<AuthResponse>(_localizer[SharedResourceKeys.RefreshTokenNotFound]);

            // Check if the refresh token is revoked or expired
            if (refreshToken.IsRevoked)
            {
                await _refreshTokenRepository.RevokeFamilyAsync(refreshToken.FamilyId);
                return Unauthorized<AuthResponse>(_localizer[SharedResourceKeys.RefreshTokenHasBeenRevoked]);
            }

            if (refreshToken.ExpiresAt < DateTime.UtcNow)
                return Unauthorized<AuthResponse>(_localizer[SharedResourceKeys.RefreshTokenExpired]);

            string rawToken = null!;
            using var transaction = await _unitOfWork.BeginTransactionAsync();
            try
            {
                // Valid refresh token, so revoke it and generate a new one and new access token
                await _refreshTokenRepository.RevokeAsync(refreshToken);
                (rawToken, var newRefreshToken) = _refreshTokenRepository.GenerateRefreshToken(refreshToken.UserId, refreshToken.FamilyId);
                await _refreshTokenRepository.AddAsync(newRefreshToken);
                await _unitOfWork.CommitAsync();
                await _unitOfWork.SaveChangesAsync();
            }
            catch (Exception)
            {
                await _unitOfWork.RollbackAsync();
                throw;
            }

            string newAccessToken = await _authenticationService.GenerateJwtTokenAsync(refreshToken.User);

            var authResponse = new AuthResponse
            {
                JwtToken = newAccessToken,
                RefreshToken = rawToken
            };

            return Success(authResponse);
        }

        public async Task<Response<string>> Handle(LogoutCommand request, CancellationToken cancellationToken)
        {
            string tokenHash = Utils.Hash(request.RefreshToken);
            var refreshToken = await _refreshTokenRepository.GetByTokenHashAsync(tokenHash);
            if (refreshToken is null)
                return Unauthorized<string>(_localizer[SharedResourceKeys.RefreshTokenNotFound]);


            await _refreshTokenRepository.RevokeFamilyAsync(refreshToken.FamilyId);
            return Success<string>(_localizer[SharedResourceKeys.LoggedOutSuccessfully]);
        }

        public async Task<Response<string>> Handle(ConfirmEmailCommand request, CancellationToken cancellationToken)
        {
            var user = await _userManager.GetByIdAsync(request.UserId);
            if (user == null)
                return NotFound<string>();
            await _authenticationService.ConfirmEmailAsync(user, request.Token);

            return Success<string>(_localizer[SharedResourceKeys.EmailConfirmedSuccessfully]);
        }

        public async Task<Response<string>> Handle(ForgotPasswordCommand request, CancellationToken cancellationToken)
        {
            var user = await _userManager.GetByEmailAsync(request.Email);

            if (user == null) return NotFound<string>();
            if (!user.EmailConfirmed) return BadRequest<string>(_localizer[SharedResourceKeys.EmailNotConfirmed]);

            using (_unitOfWork.BeginTransactionAsync())
            {
                try
                {
                    await _passwordResetCodeRepository.RevokeOldPasswordResetCodesAsync(user.Id);
                    var rawCode = _passwordResetCodeRepository.GeneratePasswordResetCode();

                    var passwordResetCode = new Data.Entities.PasswordResetCode
                    {
                        UserId = user.Id,
                        HashedCode = Utils.Hash(rawCode),
                        ExpirationDate = DateTime.UtcNow.AddMinutes(15)
                    };

                    await _passwordResetCodeRepository.AddAsync(passwordResetCode);
                    await SendPasswordResetCodeEmailAsync(user.Email, rawCode);

                    await _unitOfWork.CommitAsync();
                    await _unitOfWork.SaveChangesAsync();
                }
                catch
                {
                    await _unitOfWork.RollbackAsync();
                    throw;
                }

                return Success<string>(_localizer[SharedResourceKeys.PasswordResetCodeSentSuccessfully]);
            }
        }

        public async Task<Response<ResetPasswordUrlResponse>> Handle(VerifyResetCodeCommand request, CancellationToken cancellationToken)
        {
            var user = await _userManager.GetByEmailAsync(request.Email);
            if (user is null)
                return NotFound<ResetPasswordUrlResponse>(_localizer[SharedResourceKeys.InvalidEmailAddress]);

            var passwordResetCode = await _passwordResetCodeRepository.GetByUserIdAndCode(user.Id, request.Code);
            if (passwordResetCode is null)
                return NotFound<ResetPasswordUrlResponse>(_localizer[SharedResourceKeys.InvalidOTP]);

            if (passwordResetCode.IsRevoked || passwordResetCode.ExpirationDate < DateTime.UtcNow)
                return BadRequest<ResetPasswordUrlResponse>(_localizer[SharedResourceKeys.InvalidOTP]);

            string encodedUserId = Utils.Encode(user.Id.ToString());
            string encodedCode = Utils.Encode(request.Code);

            var resetPasswordUrl = _httpContextAccessor.HttpContext.Request.Scheme + "://" + _httpContextAccessor.HttpContext.Request.Host + "/" + Router.Authentication.ResetPassword + "?encodedUserId=" + encodedUserId + "&encodedCode=" + encodedCode;

            return Success(new ResetPasswordUrlResponse(resetPasswordUrl));
        }

        public async Task<Response<string>> Handle(ResetPasswordCommand request, CancellationToken cancellationToken)
        {
            var decodedUserId = Utils.Decode(request.EncodedUserId);
            var userId = int.Parse(decodedUserId);
            var user = await _userManager.GetByIdAsync(userId);

            if (user is null)
                return NotFound<string>();

            try
            {
                var decodedCode = Utils.Decode(request.EncodedCode);
                await _authenticationService.ResetPasswordAsync(user, decodedCode, request.NewPassword);
            }
            catch (Exception ex)
            {
                return BadRequest<string>(ex.Message);
            }

            return Success<string>(_localizer[SharedResourceKeys.ResetPasswordSuccessfully]);
        }
        #endregion
    }
}