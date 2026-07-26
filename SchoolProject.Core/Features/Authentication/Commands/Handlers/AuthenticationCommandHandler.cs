using System.Security;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Localization;
using SchoolProject.Core.Bases;
using SchoolProject.Core.Features.Authentication.Commands.Models;
using SchoolProject.Core.Features.Authentication.Commands.Responses;
using SchoolProject.Service.Abstracts;
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
        IRequestHandler<RegisterCommand, Response<string>>
    {
        #region Private Fields
        private readonly IAuthenticationService _authenticationService;
        private readonly IApplicationUserService _applicationUserService;
        private readonly IPasswordResetCodeService _passwordResetCodeService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        #endregion

        #region Constructors
        public AuthenticationCommandHandler(
            IApplicationUserService applicationUserService,
            IAuthenticationService authenticationService,
            IMapper mapper,
            IStringLocalizer<SharedResource> localizer,
            IHttpContextAccessor httpContextAccessor,
            IPasswordResetCodeService passwordResetCodeService)
            : base(localizer, mapper)
        {
            _applicationUserService = applicationUserService;
            _authenticationService = authenticationService;
            _httpContextAccessor = httpContextAccessor;
            _passwordResetCodeService = passwordResetCodeService;

        }
        #endregion

        #region Public Methods
        public async Task<Response<string>> Handle(RegisterCommand request, CancellationToken cancellationToken)
        {
            var applicationUser = _mapper.Map<Data.Entities.Identities.ApplicationUser>(request);

            var confirmationUrlTemplate = $"{_httpContextAccessor.HttpContext.Request.Scheme}://{_httpContextAccessor.HttpContext.Request.Host}/{Router.Authentication.ConfirmEmail}?userId={{0}}&token={{1}}";
            await _authenticationService.RegisterAndSendConfirmationEmailAsync(applicationUser, request.Password, confirmationUrlTemplate);

            return Created<string>(_localizer[SharedResourceKeys.AddedSuccessfully]);
        }
        public async Task<Response<AuthResponse>> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            var user = await _applicationUserService.GetByUserNameAndPasswordAsync(request.UserName, request.Password);
            if (user is null)
                return BadRequest<AuthResponse>(_localizer[SharedResourceKeys.InvalidUserNameOrPassword]);

            if (!user.EmailConfirmed)
                return Unauthorized<AuthResponse>(_localizer[SharedResourceKeys.EmailDoesNotConfirmed]);

            var accessToken = await _authenticationService.GenerateJwtTokenAsync(user);
            var (rawToken, refreshToken) = _authenticationService.GenerateRefreshToken(user.Id);
            await _authenticationService.AddRefreshTokenAsync(refreshToken);

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
            var refreshToken = await _authenticationService.GetRefreshTokenByTokenHashAsync(tokenHash);
            if (refreshToken is null)
                return Unauthorized<AuthResponse>(_localizer[SharedResourceKeys.RefreshTokenNotFound]);

            // Check if the refresh token is revoked or expired
            if (refreshToken.IsRevoked)
            {
                await _authenticationService.RevokeRefreshTokenFamilyAsync(refreshToken.FamilyId);
                return Unauthorized<AuthResponse>(_localizer[SharedResourceKeys.RefreshTokenHasBeenRevoked]);
            }

            if (refreshToken.ExpiresAt < DateTime.UtcNow)
                return Unauthorized<AuthResponse>(_localizer[SharedResourceKeys.RefreshTokenExpired]);

            // Valid refresh token, so revoke it and generate a new one and new access token
            await _authenticationService.RevokeRefreshTokenAsync(refreshToken);
            var (rawToken, newRefreshToken) = _authenticationService.GenerateRefreshToken(refreshToken.UserId, refreshToken.FamilyId);
            await _authenticationService.AddRefreshTokenAsync(newRefreshToken);

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
            var refreshToken = await _authenticationService.GetRefreshTokenByTokenHashAsync(tokenHash);
            if (refreshToken is null)
                return Unauthorized<string>(_localizer[SharedResourceKeys.RefreshTokenNotFound]);

            await _authenticationService.RevokeRefreshTokenFamilyAsync(refreshToken.FamilyId);
            return Success<string>(_localizer[SharedResourceKeys.LoggedOutSuccessfully]);
        }

        public async Task<Response<string>> Handle(ConfirmEmailCommand request, CancellationToken cancellationToken)
        {
            var user = await _applicationUserService.GetByIdAsync(request.UserId);
            if (user == null)
                return NotFound<string>();
            await _authenticationService.ConfirmEmailAsync(user, request.Token);

            return Success<string>(_localizer[SharedResourceKeys.EmailConfirmedSuccessfully]);
        }

        public async Task<Response<string>> Handle(ForgotPasswordCommand request, CancellationToken cancellationToken)
        {
            var user = await _applicationUserService.GetByEmailAsync(request.Email);

            if (user == null) return NotFound<string>();
            if (!user.EmailConfirmed) return BadRequest<string>(_localizer[SharedResourceKeys.EmailNotConfirmed]);

            await _authenticationService.GenerateAndSendPasswordResetCodeAsync(user);

            return Success<string>(_localizer[SharedResourceKeys.PasswordResetCodeSentSuccessfully]);

        }
        #endregion
    }
}