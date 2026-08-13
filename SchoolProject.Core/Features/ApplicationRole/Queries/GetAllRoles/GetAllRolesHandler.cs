using AutoMapper;
using MediatR;
using Microsoft.Extensions.Localization;
using SchoolProject.Core.Bases;
using SchoolProject.Core.Interfaces.IdentityServices;
using SchoolProject.Shared.Resources;

namespace SchoolProject.Core.Features.ApplicationRole.Queries.GetAllRoles;

public class GetAllRolesHandler : ResponseHandler, IRequestHandler<GetAllRolesQuery, Response<List<GetAllRolesQueryResponse>>>
{
    #region Private Fields
    private readonly IRoleManager _roleManager;
    #endregion

    #region Constructors
    public GetAllRolesHandler(
        IStringLocalizer<SharedResource> localizer,
        IMapper mapper,
        IRoleManager roleManager)
        : base(localizer, mapper)
    {
        _roleManager = roleManager;
    }
    #endregion

    #region Public Methods
    public async Task<Response<List<GetAllRolesQueryResponse>>> Handle(GetAllRolesQuery request, CancellationToken cancellationToken)
    {
        var roles = await _roleManager.GetAllAsync();
        var response = _mapper.Map<List<GetAllRolesQueryResponse>>(roles);
        return Success(response);
    }
    #endregion
}