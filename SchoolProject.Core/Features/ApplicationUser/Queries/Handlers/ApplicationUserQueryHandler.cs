using AutoMapper;
using MediatR;
using Microsoft.Extensions.Localization;
using SchoolProject.Core.Bases;
using SchoolProject.Core.Features.ApplicationUser.Queries.Models;
using SchoolProject.Core.Features.ApplicationUser.Queries.Responses;
using SchoolProject.Core.Responses;
using SchoolProject.Service.Abstracts;
using SchoolProject.Shared.Resources;

namespace SchoolProject.Core.Features.ApplicationUser.Queries.Handlers;

public class ApplicationUserQueryHandler :
    ResponseHandler,
    IRequestHandler<GetPaginatedApplicationUsersQuery, PaginatedResponse<GetPaginatedApplicationUsersQueryResponse>>,
    IRequestHandler<GetApplicationUserByIdQuery, Response<GetApplicationUserByIdQueryResponse>>
{
    private readonly IApplicationUserService _applicationUserService;
    public ApplicationUserQueryHandler(
        IApplicationUserService ApplicationUserService,
        IMapper mapper,
        IStringLocalizer<SharedResource> localizer) : base(localizer, mapper)
    {
        _applicationUserService = ApplicationUserService;
    }

    public async Task<PaginatedResponse<GetPaginatedApplicationUsersQueryResponse>> Handle(
        GetPaginatedApplicationUsersQuery request,
        CancellationToken cancellationToken)
    {
        int pageNumber = request.PageNumber < 0 ? 1 : request.PageNumber;
        int pageSize = request.PageSize <= 0 ? 10 : request.PageSize;

        int totalRecords =
            await _applicationUserService.GetTotalApplicationUsersCountAsync();

        var applicationUsers =
            await _applicationUserService.GetPaginatedApplicationUsersAsync(pageNumber, pageSize);

        var applicationUsersDto =
            _mapper.Map<List<GetPaginatedApplicationUsersQueryResponse>>(applicationUsers);

        var paginatedResponse = new PaginatedResponse<GetPaginatedApplicationUsersQueryResponse>(
            applicationUsersDto,
            pageNumber,
            pageSize,
            totalRecords);

        return paginatedResponse;
    }

    public async Task<Response<GetApplicationUserByIdQueryResponse>> Handle(GetApplicationUserByIdQuery request, CancellationToken cancellationToken)
    {
        var appUser = await _applicationUserService.GetApplicationUserByIdAsync(request.Id);

        if (appUser is null)
            return NotFound<GetApplicationUserByIdQueryResponse>(_localizer[SharedResourceKeys.NotFound]);

        var result = _mapper.Map<GetApplicationUserByIdQueryResponse>(appUser);

        return Success(result);
    }
}