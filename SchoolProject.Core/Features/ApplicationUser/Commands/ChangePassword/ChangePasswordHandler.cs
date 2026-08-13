using AutoMapper;
using MediatR;
using Microsoft.Extensions.Localization;
using SchoolProject.Core.Bases;
using SchoolProject.Core.Interfaces.IdentityServices;
using SchoolProject.Shared.Resources;

namespace SchoolProject.Core.Features.ApplicationUser.Commands.ChangePassword;

public class ChangePasswordHandler : ResponseHandler, IRequestHandler<ChangePasswordCommand, Response<string>>
{
    #region Private Fields
    private readonly IUserManager _userManager;
    #endregion

    #region Constructors
    public ChangePasswordHandler(
        IStringLocalizer<SharedResource> localizer,
        IMapper mapper,
        IUserManager userManager)
        : base(localizer, mapper)
    {
        _userManager = userManager;
    }
    #endregion

    #region Public Methods
    public async Task<Response<string>> Handle(ChangePasswordCommand request, CancellationToken cancellationToken)
    {
        await _userManager.ChangePasswordAsync(request.Id, request.CurrentPassword, request.NewPassword);
        return Success<string>(_localizer[SharedResourceKeys.UpdatedSuccessfully]);
    }
    #endregion
}