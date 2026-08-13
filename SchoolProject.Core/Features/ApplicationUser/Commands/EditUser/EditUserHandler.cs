using AutoMapper;
using MediatR;
using Microsoft.Extensions.Localization;
using SchoolProject.Core.Bases;
using SchoolProject.Core.Interfaces.IdentityServices;
using SchoolProject.Shared.Resources;

namespace SchoolProject.Core.Features.ApplicationUser.Commands.EditUser;

public class EditUserHandler : ResponseHandler, IRequestHandler<EditUserCommand, Response<string>>
{
    #region Private Fields
    private readonly IUserManager _userManager;
    #endregion

    #region Constructors
    public EditUserHandler(
        IStringLocalizer<SharedResource> localizer,
        IMapper mapper,
        IUserManager userManager)
        : base(localizer, mapper)
    {
        _userManager = userManager;
    }
    #endregion

    #region Public Methods
    public async Task<Response<string>> Handle(EditUserCommand request, CancellationToken cancellationToken)
    {
        var user = await _userManager.GetByIdAsync(request.Id);
        _mapper.Map(request, user);
        await _userManager.UpdateAsync(user!);
        return Success<string>(_localizer[SharedResourceKeys.UpdatedSuccessfully]);
    }
    #endregion
}