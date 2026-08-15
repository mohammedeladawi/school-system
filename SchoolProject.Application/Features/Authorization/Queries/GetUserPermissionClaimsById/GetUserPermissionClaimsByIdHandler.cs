using AutoMapper;
using MediatR;
using Microsoft.Extensions.Localization;
using SchoolProject.Application.Bases;
using SchoolProject.Application.Interfaces.IdentityServices;
using SchoolProject.Application.Resources;

namespace SchoolProject.Application.Features.Authorization.Queries.GetUserPermissionClaimsById;

public class GetUserPermissionClaimsByIdHandler : ResponseHandler, IRequestHandler<GetUserPermissionClaimsByIdQuery, Response<GetUserPermissionClaimsByIdQueryResponse>>
{
    #region Fields
    private readonly IAuthorizationService _authorizationService;
    #endregion

    #region Constructors
    public GetUserPermissionClaimsByIdHandler(
        IStringLocalizer<SharedResource> localizer,
        IMapper mapper,
        IAuthorizationService authorizationService
        ) : base(localizer, mapper)
    {
        _authorizationService = authorizationService;
    }
    #endregion

    #region Private Methods
    private List<PermissionClaims> BuildUserPermissionClaimsResponse(List<string> claimValues)
    {
        var permissionClaimsList = new List<PermissionClaims>();
        foreach (var permissionName in Domain.ClaimStore.PermissionClaims.UserPermissionClaims)
        {
            var pClaim = new PermissionClaims
            {
                Name = permissionName,
                Value = claimValues.Any(cv => cv == permissionName)
            };

            permissionClaimsList.Add(pClaim);
        }

        return permissionClaimsList;
    }

    #endregion

    #region Public Methods
    public async Task<Response<GetUserPermissionClaimsByIdQueryResponse>> Handle(GetUserPermissionClaimsByIdQuery request, CancellationToken cancellationToken)
    {
        var userPermissionClaims = await _authorizationService.GetUserPermissionsAsync(request.UserId);

        var response = new GetUserPermissionClaimsByIdQueryResponse
        {
            UserId = request.UserId,
            UserPermissionClaims = BuildUserPermissionClaimsResponse(userPermissionClaims.ToList())
        };

        return Success(response);
    }
    #endregion
}