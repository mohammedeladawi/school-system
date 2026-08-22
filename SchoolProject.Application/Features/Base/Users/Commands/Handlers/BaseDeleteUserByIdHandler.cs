using AutoMapper;
using MediatR;
using Microsoft.Extensions.Localization;
using SchoolProject.Application.Bases;
using SchoolProject.Application.Features.Base.Users.Commands.RequestDTOs;
using SchoolProject.Application.Interfaces.Bases;
using SchoolProject.Application.Resources;

namespace SchoolProject.Application.Features.Base.Users.Commands.Handlers;

public class BaseDeleteUserByIdHandler<TCommand, TManager, TUser> :
    ResponseHandler,
    IRequestHandler<TCommand, Response<string>>
    where TCommand : BaseDeleteUserByIdCommand
    where TManager : IGenericIdentityUserManagerAsync<TUser>
    where TUser : Domain.Entities.Identities.ApplicationUser
{
    #region Private Fields

    private readonly TManager _userManager;

    #endregion

    #region Constructors

    public BaseDeleteUserByIdHandler(
        IStringLocalizer<SharedResource> localizer,
        IMapper mapper,
        TManager userManager)
        : base(localizer, mapper)
    {
        _userManager = userManager;
    }

    #endregion

    #region Public Methods

    public async Task<Response<string>> Handle(
        TCommand request,
        CancellationToken cancellationToken)
    {
        var user = await _userManager.GetByIdAsync(request.Id);
        if (user is null)
            return NotFound<string>();

        await _userManager.DeleteAsync(user);
        return Deleted<string>();
    }

    #endregion
}