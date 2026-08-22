using System.Linq.Expressions;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Localization;
using SchoolProject.Application.Bases;
using SchoolProject.Application.Features.Base.Users.Queries.RequestDTOs;
using SchoolProject.Application.Features.Base.Users.Queries.ResponseDTOs;
using SchoolProject.Application.Interfaces.Bases;
using SchoolProject.Application.Resources;

namespace SchoolProject.Application.Features.Base.Users.Queries.Handlers;

public class BaseGetPaginatedUsersHandler<TQuery, TResponse, TManager, TUser> :
    ResponseHandler,
    IRequestHandler<TQuery, PaginatedResponse<TResponse>>
    where TQuery : BaseGetPaginatedUsersQuery<TResponse>
    where TResponse : BaseGetPaginatedUsersResponse
    where TManager : IGenericIdentityUserManagerAsync<TUser>
    where TUser : Domain.Entities.Identities.ApplicationUser
{
    #region Protected Fields
    protected readonly TManager _userManager;
    #endregion

    #region Constructors
    public BaseGetPaginatedUsersHandler(
        IMapper mapper,
        IStringLocalizer<SharedResource> localizer,
        TManager userManager) : base(localizer, mapper)
    {
        _userManager = userManager;
    }
    #endregion

    #region Protected Methods
    protected virtual Expression<Func<TUser, object>>[]? GetIncludes() => null;
    #endregion

    #region Public Methods
    public async Task<PaginatedResponse<TResponse>> Handle(
        TQuery request,
        CancellationToken cancellationToken)
    {
        int pageNumber = request.PageNumber < 0 ? 1 : request.PageNumber;
        int pageSize = (request.PageSize <= 0 || request.PageSize >= 20) ? 20 : request.PageSize;

        int totalRecords = await _userManager.GetTotalCountAsync();

        var applicationUsers = await _userManager.GetPaginatedListAsync(pageNumber, pageSize, GetIncludes());

        var applicationUsersDto = _mapper.Map<List<TResponse>>(applicationUsers);

        var paginatedResponse = new PaginatedResponse<TResponse>(
            applicationUsersDto,
            pageNumber,
            pageSize,
            totalRecords);

        return paginatedResponse;
    }
    #endregion
}