using AutoMapper;
using MediatR;
using Microsoft.Extensions.Localization;
using SchoolProject.Core.Bases;
using SchoolProject.Core.Features.Authentication.Commands.Models;
using SchoolProject.Service.Abstracts;
using SchoolProject.Shared.Resources;

namespace SchoolProject.Core.Features.Authentication.Commands.Handlers
{
    public class AuthenticationCommandHandler :
        ResponseHandler,
        IRequestHandler<LoginCommand, Response<string>>
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
        public async Task<Response<string>> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            var user = await _applicationUserService.GetByUserNameAndPasswordAsync(request.UserName, request.Password);
            if (user is null)
                return BadRequest<string>(_localizer[SharedResourceKeys.InvalidUserNameOrPassword]);

            var accessToken = _authenticationService.GenerateJwtToken(user);
            return Success(accessToken);
        }
        #endregion
    }
}