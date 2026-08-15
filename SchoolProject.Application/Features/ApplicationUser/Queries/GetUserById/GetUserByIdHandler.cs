using AutoMapper;
using MediatR;
using Microsoft.Extensions.Localization;
using SchoolProject.Application.Bases;
using SchoolProject.Application.Interfaces.IdentityServices;
using SchoolProject.Shared.Resources;

namespace SchoolProject.Application.Features.ApplicationUser.Queries.GetUserById;

public class GetUserByIdHandler : ResponseHandler, IRequestHandler<GetUserByIdQuery, Response<GetUserByIdQueryResponse>>
{
    #region Private Fields
    private readonly IUserManager _userManager;
    #endregion

    #region Constructors
    public GetUserByIdHandler(
        IUserManager userManager,
        IMapper mapper,
        IStringLocalizer<SharedResource> localizer) : base(localizer, mapper)
    {
        _userManager = userManager;
    }
    #endregion

    #region Public Methods
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