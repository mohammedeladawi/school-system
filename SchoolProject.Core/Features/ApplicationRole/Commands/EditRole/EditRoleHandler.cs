using AutoMapper;
using MediatR;
using Microsoft.Extensions.Localization;
using SchoolProject.Core.Bases;
using SchoolProject.Core.Interfaces.IdentityServices;
using SchoolProject.Shared.Resources;

namespace SchoolProject.Core.Features.ApplicationRole.Commands.EditRole;

public class EditRoleHandler : ResponseHandler, IRequestHandler<EditRoleCommand, Response<string>>
{
    #region Private Fields
    private readonly IRoleManager _roleManager;
    #endregion

    #region Constructors
    public EditRoleHandler(
        IStringLocalizer<SharedResource> localizer,
        IMapper mapper,
        IRoleManager roleManager)
        : base(localizer, mapper)
    {
        _roleManager = roleManager;
    }
    #endregion

    #region Public Methods
    public async Task<Response<string>> Handle(EditRoleCommand request, CancellationToken cancellationToken)
    {
        var role = await _roleManager.GetByIdAsync(request.Id);
        role!.Name = request.NewName;
        await _roleManager.EditAsync(role);
        return Success<string>(_localizer[SharedResourceKeys.UpdatedSuccessfully]);
    }
    #endregion
}