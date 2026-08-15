using AutoMapper;
using MediatR;
using Microsoft.Extensions.Localization;
using SchoolProject.Application.Bases;
using SchoolProject.Application.Interfaces.IdentityServices;
using SchoolProject.Shared.Resources;

namespace SchoolProject.Application.Features.ApplicationUser.Queries.GetPaginatedUsers;

public class GetPaginatedUsersHandler : ResponseHandler, IRequestHandler<GetPaginatedUsersQuery, PaginatedResponse<GetPaginatedUsersQueryResponse>>
{
    #region Private Fields
    private readonly IUserManager _userManager;
    #endregion

    #region Constructors
    public GetPaginatedUsersHandler(
        IUserManager userManager,
        IMapper mapper,
        IStringLocalizer<SharedResource> localizer) : base(localizer, mapper)
    {
        _userManager = userManager;
    }
    #endregion

    #region Public Methods
    public async Task<PaginatedResponse<GetPaginatedUsersQueryResponse>> Handle(
        GetPaginatedUsersQuery request,
        CancellationToken cancellationToken)
    {
        int pageNumber = request.PageNumber < 0 ? 1 : request.PageNumber;
        int pageSize = (request.PageSize <= 0 || request.PageSize >= 20) ? 20 : request.PageSize;

        int totalRecords = await _userManager.GetTotalCountAsync();

        var applicationUsers = await _userManager.GetPaginatedListAsync(pageNumber, pageSize);

        var applicationUsersDto = _mapper.Map<List<GetPaginatedUsersQueryResponse>>(applicationUsers);

        var paginatedResponse = new PaginatedResponse<GetPaginatedUsersQueryResponse>(
            applicationUsersDto,
            pageNumber,
            pageSize,
            totalRecords);

        return paginatedResponse;
    }
    #endregion
}