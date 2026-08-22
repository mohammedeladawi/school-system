using AutoMapper;
using MediatR;
using Microsoft.Extensions.Localization;
using SchoolProject.Application.Bases;
using SchoolProject.Application.Features.Base.Users.Commands.RequestDTOs;
using SchoolProject.Application.Interfaces.Bases;
using SchoolProject.Application.Resources;

namespace SchoolProject.Application.Features.Base.Users.Commands.Handlers;

public class BaseChangePasswordHandler<TCommand, TManager, TUser> :
    ResponseHandler, IRequestHandler<TCommand, Response<string>>
    where TCommand : BaseChangePasswordCommand
    where TManager : IGenericIdentityUserManagerAsync<TUser>
    where TUser : Domain.Entities.Identities.ApplicationUser
{
    #region Private Fields
    private readonly TManager _userManager;
    #endregion

    #region Constructors
    public BaseChangePasswordHandler(
        IStringLocalizer<SharedResource> localizer,
        IMapper mapper,
        TManager userManager)
        : base(localizer, mapper)
    {
        _userManager = userManager;
    }
    #endregion

    #region Public Methods
    public async Task<Response<string>> Handle(TCommand request, CancellationToken cancellationToken)
    {
        var isValid = await _userManager.CheckPasswordAsync(request.Id, request.CurrentPassword);
        if (!isValid)
            return BadRequest<string>(_localizer[SharedResourceKeys.InvalidCurrentPassword]);

        await _userManager.ChangePasswordAsync(request.Id, request.CurrentPassword, request.NewPassword);
        return Success<string>(_localizer[SharedResourceKeys.UpdatedSuccessfully]);
    }
    #endregion
}