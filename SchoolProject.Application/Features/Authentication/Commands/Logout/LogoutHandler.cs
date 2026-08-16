using AutoMapper;
using MediatR;
using Microsoft.Extensions.Localization;
using SchoolProject.Application.Bases;
using SchoolProject.Application.Interfaces.Bases;
using SchoolProject.Application.Interfaces.Repositories;
using SchoolProject.Application.Helpers;
using SchoolProject.Application.Resources;

namespace SchoolProject.Application.Features.Authentication.Commands.Logout
{
    public class LogoutHandler : ResponseHandler, IRequestHandler<LogoutCommand, Response<string>>
    {
        #region Private Fields
        private readonly IRefreshTokenRepository _refreshTokenRepository;
        #endregion

        #region Constructors
        public LogoutHandler(
            IMapper mapper,
            IStringLocalizer<SharedResource> localizer,
            IRefreshTokenRepository refreshTokenRepository)
            : base(localizer, mapper)
        {
            _refreshTokenRepository = refreshTokenRepository;
        }
        #endregion

        #region Public Methods
        public async Task<Response<string>> Handle(LogoutCommand request, CancellationToken cancellationToken)
        {
            var refreshToken = await _refreshTokenRepository.GetByTokenAsync(request.RefreshToken);
            if (refreshToken is null)
                return Unauthorized<string>(_localizer[SharedResourceKeys.RefreshTokenNotFound]);

            await _refreshTokenRepository.RevokeFamilyAsync(refreshToken.FamilyId);
            return Success<string>(_localizer[SharedResourceKeys.LoggedOutSuccessfully]);
        }
        #endregion
    }
}
