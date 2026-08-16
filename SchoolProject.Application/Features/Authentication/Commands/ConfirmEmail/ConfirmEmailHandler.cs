using AutoMapper;
using MediatR;
using Microsoft.Extensions.Localization;
using SchoolProject.Application.Bases;
using SchoolProject.Application.Interfaces.Bases;
using SchoolProject.Application.Interfaces.IdentityServices;
using SchoolProject.Application.Helpers;
using SchoolProject.Application.Resources;

namespace SchoolProject.Application.Features.Authentication.Commands.ConfirmEmail
{
    public class ConfirmEmailHandler : ResponseHandler, IRequestHandler<ConfirmEmailCommand, Response<string>>
    {
        #region Private Fields
        private readonly IUserManager _userManager;
        #endregion

        #region Constructors
        public ConfirmEmailHandler(
            IUserManager userManager,
            IMapper mapper,
            IStringLocalizer<SharedResource> localizer)
            : base(localizer, mapper)
        {
            _userManager = userManager;
        }
        #endregion

        #region Public Methods
        public async Task<Response<string>> Handle(ConfirmEmailCommand request, CancellationToken cancellationToken)
        {
            var user = await _userManager.GetByIdAsync(request.UserId);
            if (user == null) return NotFound<string>();

            string decodedToken = Utils.Decode(request.EncodedToken);
            await _userManager.ConfirmEmailAsync(user, decodedToken);

            return Success<string>(_localizer[SharedResourceKeys.EmailConfirmedSuccessfully]);
        }
        #endregion
    }
}
