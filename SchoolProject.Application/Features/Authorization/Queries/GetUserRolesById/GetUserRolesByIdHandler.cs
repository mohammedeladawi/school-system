using AutoMapper;
using MediatR;
using Microsoft.Extensions.Localization;
using SchoolProject.Application.Bases;
using SchoolProject.Application.Interfaces.IdentityServices;
using SchoolProject.Application.Interfaces.Services;
using SchoolProject.Shared.Resources;

namespace SchoolProject.Application.Features.Authorization.Queries.GetUserRolesById;

public class GetUserRolesByIdHandler : ResponseHandler, IRequestHandler<GetUserRolesByIdQuery, Response<GetUserRolesByIdQueryResponse>>
{
    #region Fields
    private readonly IAuthorizationService _authorizationService;
    private readonly IRoleManager _roleManager;
    #endregion

    #region Constructors
    public GetUserRolesByIdHandler(
        IStringLocalizer<SharedResource> localizer,
        IMapper mapper,
        IAuthorizationService authorizationService,
        IRoleManager roleManager
        ) : base(localizer, mapper)
    {
        _authorizationService = authorizationService;
        _roleManager = roleManager;
    }
    #endregion

    #region Private Methods
    private async Task<List<RoleResponse>> BuildUserRolesResponse(
        List<string> userRoles,
        List<Data.Entities.Identities.ApplicationRole> allRoles)
    {
        var rolesResponse = new List<RoleResponse>();

        // iterate over them
        foreach (var role in allRoles)
        {
            var roleResponse = new RoleResponse
            {
                Id = role.Id,
                Name = role.Name!,
                HasRole = userRoles.Contains(role.Name!)
            };

            rolesResponse.Add(roleResponse);
        }

        return rolesResponse;
    }
    #endregion

    #region Public Methods
    public async Task<Response<GetUserRolesByIdQueryResponse>> Handle(GetUserRolesByIdQuery request, CancellationToken cancellationToken)
    {
        var userRoles = await _authorizationService.GetUserRolesAsync(request.UserId);
        var allRoles = await _roleManager.GetAllAsync();
        var rolesResponse = await BuildUserRolesResponse(userRoles.ToList(), allRoles);

        var response = new GetUserRolesByIdQueryResponse
        {
            UserId = request.UserId,
            Roles = rolesResponse
        };

        return Success(response);
    }
    #endregion
}