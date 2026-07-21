using System.Security;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Localization;
using SchoolProject.Core.Bases;
using SchoolProject.Core.Features.Authentication.Commands.Models;
using SchoolProject.Core.Features.Authentication.Commands.Responses;
using SchoolProject.Service.Abstracts;
using SchoolProject.Shared.Helpers;
using SchoolProject.Shared.Resources;

namespace SchoolProject.Core.Features.Authentication.Commands.Handlers
{
    public class AuthenticationCommandHandler :
        ResponseHandler,
        IRequestHandler<LoginCommand, Response<AuthResponse>>,
        IRequestHandler<RefreshTokenCommand, Response<AuthResponse>>,
        IRequestHandler<LogoutCommand, Response<string>>
    {
        #region Private Fields
        private readonly IAuthenticationService _authenticationService;
        private readonly IApplicationUserService _applicationUserService;
        #endregion

        #region Constructors
        public AuthenticationCommandHandler(
            IApplicationUserService applicationUserService,
            IAuthenticationService authenticationService,
            IMapper mapper,
            IStringLocalizer<SharedResource> localizer)
            : base(localizer, mapper)
        {
            _applicationUserService = applicationUserService;
            _authenticationService = authenticationService;
        }
        #endregion

        #region Public Methods
        public async Task<Response<AuthResponse>> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            var user = await _applicationUserService.GetByUserNameAndPasswordAsync(request.UserName, request.Password);
            if (user is null)
                return BadRequest<AuthResponse>(_localizer[SharedResourceKeys.InvalidUserNameOrPassword]);

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
        #endregion
    }
}