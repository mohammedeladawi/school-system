using AutoMapper;
using MediatR;
using Microsoft.Extensions.Localization;
using SchoolProject.Core.Bases;
using SchoolProject.Core.Interfaces.IdentityServices;
using SchoolProject.Core.Interfaces.Services;
using SchoolProject.Shared.Resources;

namespace SchoolProject.Core.Features.Authorization.Commands.UpdateUserRoles;

public class UpdateUserRolesHandler : ResponseHandler, IRequestHandler<UpdateUserRolesCommand, Response<string>>
{
    #region Fields
    private readonly IAuthorizationService _authorizationService;
    private readonly IUserManager _userManager;
    #endregion

    #region Constructors
    public UpdateUserRolesHandler(
        IStringLocalizer<SharedResource> localizer,
        IMapper mapper,
        IAuthorizationService authorizationService,
        IUserManager userManager
        ) : base(localizer, mapper)
    {
        _authorizationService = authorizationService;
        _userManager = userManager;
    }
    #endregion

    #region Public Methods
    public async Task<Response<string>> Handle(UpdateUserRolesCommand request, CancellationToken cancellationToken)
    {
        var user = await _userManager.GetByIdAsync(request.UserId);
        await _authorizationService.UpdateUserRoles(user, request.RoleNames);
        return Success<string>(_localizer[SharedResourceKeys.UpdatedSuccessfully]);
    }
    #endregion
}