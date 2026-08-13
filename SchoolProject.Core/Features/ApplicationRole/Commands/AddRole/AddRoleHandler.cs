using AutoMapper;
using MediatR;
using Microsoft.Extensions.Localization;
using SchoolProject.Core.Bases;
using SchoolProject.Core.Interfaces.IdentityServices;
using SchoolProject.Shared.Resources;

namespace SchoolProject.Core.Features.ApplicationRole.Commands.AddRole;

public class AddRoleHandler : ResponseHandler, IRequestHandler<AddRoleCommand, Response<string>>
{
    #region Private Fields
    private readonly IRoleManager _roleManager;
    #endregion

    #region Constructors
    public AddRoleHandler(
        IStringLocalizer<SharedResource> localizer,
        IMapper mapper,
        IRoleManager roleManager)
        : base(localizer, mapper)
    {
        _roleManager = roleManager;
    }
    #endregion

    #region Public Methods
    public async Task<Response<string>> Handle(AddRoleCommand request, CancellationToken cancellationToken)
    {
        await _roleManager.CreateAsync(request.RoleName);
        return Created<string>(_localizer[SharedResourceKeys.AddedSuccessfully]);
    }
    #endregion
}