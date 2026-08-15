using AutoMapper;
using MediatR;
using Microsoft.Extensions.Localization;
using SchoolProject.Application.Bases;
using SchoolProject.Application.Interfaces.Bases;
using SchoolProject.Application.Interfaces.IdentityServices;
using SchoolProject.Application.Interfaces.Repositories;
using SchoolProject.Shared.Resources;

namespace SchoolProject.Application.Features.Authentication.Commands.Login
{
    public class LoginHandler : ResponseHandler, IRequestHandler<LoginCommand, Response<AuthResponse>>
    {
        #region Private Fields
        private readonly IUserManager _userManager;
        private readonly IJwtService _jwtService;
        private readonly IRefreshTokenRepository _refreshTokenRepository;
        private readonly IUnitOfWork _unitOfWork;
        #endregion

        #region Constructors
        public LoginHandler(
            IUserManager userManager,
            IJwtService jwtService,
            IMapper mapper,
            IStringLocalizer<SharedResource> localizer,
            IRefreshTokenRepository refreshTokenRepository,
            IUnitOfWork unitOfWork)
            : base(localizer, mapper)
        {
            _userManager = userManager;
            _jwtService = jwtService;
            _refreshTokenRepository = refreshTokenRepository;
            _unitOfWork = unitOfWork;
        }
        #endregion

        #region Public Methods
        public async Task<Response<AuthResponse>> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            var user = await _userManager.GetByUserNameAndPasswordAsync(request.UserName, request.Password);
            if (user is null)
                return BadRequest<AuthResponse>(_localizer[SharedResourceKeys.InvalidUserNameOrPassword]);

            if (!user.EmailConfirmed)
                return Unauthorized<AuthResponse>(_localizer[SharedResourceKeys.EmailDoesNotConfirmed]);

            var accessToken = await _jwtService.GenerateJwtTokenAsync(user);
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

        #endregion
    }
}
