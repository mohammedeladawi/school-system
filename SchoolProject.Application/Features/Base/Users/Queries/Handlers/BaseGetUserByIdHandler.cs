using System.Linq.Expressions;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Localization;
using SchoolProject.Application.Bases;
using SchoolProject.Application.Features.Base.ApplicationUser.Queries.ResponseDTOs;
using SchoolProject.Application.Features.Base.Users.Queries.RequestDTOs;
using SchoolProject.Application.Interfaces.Bases;
using SchoolProject.Application.Resources;

namespace SchoolProject.Application.Features.ApplicationUser.Queries.GetUserById;

public class BaseGetUserByIdHandler<TRequest, TResponse, TManager, TUser> :
    ResponseHandler,
    IRequestHandler<TRequest, Response<TResponse>>
    where TRequest : BaseGetUserByIdQuery<TResponse>
    where TResponse : BaseGetUserByIdResponse
    where TManager : IGenericIdentityUserManagerAsync<TUser>
    where TUser : Domain.Entities.Identities.ApplicationUser
{
    #region Protected Fields
    protected readonly TManager _userManager;
    #endregion

    #region Constructors
    public BaseGetUserByIdHandler(
        TManager userManager,
        IMapper mapper,
        IStringLocalizer<SharedResource> localizer) : base(localizer, mapper)
    {
        _userManager = userManager;
    }
    #endregion

    #region Protected Methods
    protected virtual Expression<Func<TUser, object>>[]? GetIncludes() => null;
    #endregion

    #region Public Methods
    public async Task<Response<TResponse>> Handle(TRequest request, CancellationToken cancellationToken)
    {
        var user = await _userManager.GetByIdAsync(request.Id, GetIncludes());

        if (user is null)
            return NotFound<TResponse>(_localizer[SharedResourceKeys.NotFound]);

        var result = _mapper.Map<TResponse>(user);

        return Success(result);
    }
    #endregion
}