using AutoMapper;
using MediatR;
using Microsoft.Extensions.Localization;
using SchoolProject.Application.Bases;
using SchoolProject.Application.Interfaces.IdentityServices;
using SchoolProject.Shared.Resources;

namespace SchoolProject.Application.Features.ApplicationRole.Commands.DeleteRole;

public class DeleteRoleByIdHandler : ResponseHandler, IRequestHandler<DeleteRoleByIdCommand, Response<string>>
{
    #region Private Fields
    private readonly IRoleManager _roleManager;
    #endregion

    #region Constructors
    public DeleteRoleByIdHandler(
        IStringLocalizer<SharedResource> localizer,
        IMapper mapper,
        IRoleManager roleManager)
        : base(localizer, mapper)
    {
        _roleManager = roleManager;
    }
    #endregion

    #region Public Methods
    public async Task<Response<string>> Handle(DeleteRoleByIdCommand request, CancellationToken cancellationToken)
    {
        var role = await _roleManager.GetByIdAsync(request.Id);
        await _roleManager.DeleteAsync(role!);
        return Deleted<string>();
    }
    #endregion
}