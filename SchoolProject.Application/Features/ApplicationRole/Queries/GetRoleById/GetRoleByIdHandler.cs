using AutoMapper;
using MediatR;
using Microsoft.Extensions.Localization;
using SchoolProject.Application.Bases;
using SchoolProject.Application.Interfaces.IdentityServices;
using SchoolProject.Application.Resources;

namespace SchoolProject.Application.Features.ApplicationRole.Queries.GetRoleById;

public class GetRoleByIdHandler : ResponseHandler, IRequestHandler<GetRoleByIdQuery, Response<GetRoleByIdQueryResponse>>
{
    #region Private Fields
    private readonly IRoleManager _roleManager;
    #endregion

    #region Constructors
    public GetRoleByIdHandler(
        IStringLocalizer<SharedResource> localizer,
        IMapper mapper,
        IRoleManager roleManager)
        : base(localizer, mapper)
    {
        _roleManager = roleManager;
    }
    #endregion

    #region Public Methods
    public async Task<Response<GetRoleByIdQueryResponse>> Handle(GetRoleByIdQuery request, CancellationToken cancellationToken)
    {
        var role = await _roleManager.GetByIdAsync(request.Id);
        var response = _mapper.Map<GetRoleByIdQueryResponse>(role);
        return Success(response);
    }
    #endregion
}