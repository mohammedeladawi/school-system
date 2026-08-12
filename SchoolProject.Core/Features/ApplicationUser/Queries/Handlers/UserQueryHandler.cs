using AutoMapper;
using MediatR;
using Microsoft.Extensions.Localization;
using SchoolProject.Core.Bases;
using SchoolProject.Core.Features.ApplicationUser.Queries.Models;
using SchoolProject.Core.Features.ApplicationUser.Queries.Responses;
using SchoolProject.Core.Interfaces.IdentityServices;
using SchoolProject.Core.Responses;
using SchoolProject.Shared.Resources;

namespace SchoolProject.Core.Features.ApplicationUser.Queries.Handlers;

public class ApplicationUserQueryHandler :
    ResponseHandler,
    IRequestHandler<GetPaginatedUsersQuery, PaginatedResponse<GetPaginatedUsersQueryResponse>>,
    IRequestHandler<GetUserByIdQuery, Response<GetUserByIdQueryResponse>>
{
    #region Private Fields
    private readonly IUserManager _userManager;
    #endregion

    #region Constructors
    public ApplicationUserQueryHandler(
        IUserManager ApplicationUserRepositories,
        IMapper mapper,
        IStringLocalizer<SharedResource> localizer) : base(localizer, mapper)
    {
        _userManager = ApplicationUserRepositories;
    }
    #endregion

    #region Public Methods
    public async Task<PaginatedResponse<GetPaginatedUsersQueryResponse>> Handle(
        GetPaginatedUsersQuery request,
        CancellationToken cancellationToken)
    {
        int pageNumber = request.PageNumber < 0 ? 1 : request.PageNumber;
        int pageSize = request.PageSize <= 0 ? 10 : request.PageSize;

        int totalRecords =
            await _userManager.GetTotalCountAsync();

        var applicationUsers =
            await _userManager.GetPaginatedListAsync(pageNumber, pageSize);

        var applicationUsersDto =
            _mapper.Map<List<GetPaginatedUsersQueryResponse>>(applicationUsers);

        var paginatedResponse = new PaginatedResponse<GetPaginatedUsersQueryResponse>(
            applicationUsersDto,
            pageNumber,
            pageSize,
            totalRecords);

        return paginatedResponse;
    }

    public async Task<Response<GetUserByIdQueryResponse>> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
    {
        var appUser = await _userManager.GetByIdAsync(request.Id);

        if (appUser is null)
            return NotFound<GetUserByIdQueryResponse>(_localizer[SharedResourceKeys.NotFound]);

        var result = _mapper.Map<GetUserByIdQueryResponse>(appUser);

        return Success(result);
    }
    #endregion
}