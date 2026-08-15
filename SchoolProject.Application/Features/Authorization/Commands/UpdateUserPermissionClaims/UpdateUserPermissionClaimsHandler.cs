using AutoMapper;
using MediatR;
using Microsoft.Extensions.Localization;
using SchoolProject.Application.Bases;
using SchoolProject.Application.Interfaces.IdentityServices;
using SchoolProject.Application.Interfaces.Services;
using SchoolProject.Shared.Resources;

namespace SchoolProject.Application.Features.Authorization.Commands.UpdateUserPermissionClaims;

public class UpdateUserPermissionClaimsHandler : ResponseHandler, IRequestHandler<UpdateUserPermissionClaimsCommand, Response<string>>
{
    #region Fields
    private readonly IAuthorizationService _authorizationService;
    private readonly IUserManager _userManager;
    #endregion

    #region Constructors
    public UpdateUserPermissionClaimsHandler(
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
    public async Task<Response<string>> Handle(UpdateUserPermissionClaimsCommand request, CancellationToken cancellationToken)
    {
        var user = await _userManager.GetByIdAsync(request.UserId);
        await _authorizationService.UpdateUserPermissionClaims(user!, request.PermissionClaims);
        return Success<string>(_localizer[SharedResourceKeys.UpdatedSuccessfully]);
    }
    #endregion
}