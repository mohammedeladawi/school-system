using AutoMapper;
using MediatR;
using Microsoft.Extensions.Localization;
using SchoolProject.Application.Bases;
using SchoolProject.Application.Interfaces.Bases;
using SchoolProject.Application.Interfaces.IdentityServices;
using SchoolProject.Application.Interfaces.Repositories;
using SchoolProject.Shared.Helpers;
using SchoolProject.Shared.Resources;

namespace SchoolProject.Application.Features.Authentication.Commands.RefreshToken
{
    public class RefreshTokenHandler : ResponseHandler, IRequestHandler<RefreshTokenCommand, Response<AuthResponse>>
    {
        #region Private Fields
        private readonly IJwtService __jwtService;
        private readonly IRefreshTokenRepository _refreshTokenRepository;
        private readonly IUnitOfWork _unitOfWork;
        #endregion

        #region Constructors
        public RefreshTokenHandler(
            IJwtService _jwtService,
            IMapper mapper,
            IStringLocalizer<SharedResource> localizer,
            IRefreshTokenRepository refreshTokenRepository,
            IUnitOfWork unitOfWork)
            : base(localizer, mapper)
        {
            __jwtService = _jwtService;
            _refreshTokenRepository = refreshTokenRepository;
            _unitOfWork = unitOfWork;
        }
        #endregion

        #region Public Methods
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

            string rawToken; ;
            string newAccessToken;

            using var transaction = await _unitOfWork.BeginTransactionAsync();
            try
            {
                // revoke old one, generate a new one and save it, and generate new access token
                await _refreshTokenRepository.RevokeAsync(refreshToken);
                (rawToken, var newRefreshToken) = _refreshTokenRepository.GenerateRefreshToken(refreshToken.UserId, refreshToken.FamilyId);
                await _refreshTokenRepository.AddAsync(newRefreshToken);

                newAccessToken = await __jwtService.GenerateJwtTokenAsync(refreshToken.User);

                await _unitOfWork.CommitAsync();
                _unitOfWork.SaveChangesAsync();
            }
            catch (Exception)
            {
                await _unitOfWork.RollbackAsync();
                throw;
            }


            var authResponse = new AuthResponse
            {
                JwtToken = newAccessToken,
                RefreshToken = rawToken
            };

            return Success(authResponse);
        }
        #endregion
    }
}
