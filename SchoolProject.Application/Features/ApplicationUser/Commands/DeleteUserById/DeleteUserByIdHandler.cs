using AutoMapper;
using MediatR;
using Microsoft.Extensions.Localization;
using SchoolProject.Application.Bases;
using SchoolProject.Application.Interfaces.IdentityServices;
using SchoolProject.Shared.Resources;

namespace SchoolProject.Application.Features.ApplicationUser.Commands.DeleteUserById;

public class DeleteUserByIdHandler : ResponseHandler, IRequestHandler<DeleteUserByIdCommand, Response<string>>
{
    #region Private Fields
    private readonly IUserManager _userManager;
    #endregion

    #region Constructors
    public DeleteUserByIdHandler(
        IStringLocalizer<SharedResource> localizer,
        IMapper mapper,
        IUserManager userManager)
        : base(localizer, mapper)
    {
        _userManager = userManager;
    }
    #endregion

    #region Public Methods
    public async Task<Response<string>> Handle(DeleteUserByIdCommand request, CancellationToken cancellationToken)
    {
        var user = await _userManager.GetByIdAsync(request.Id);
        await _userManager.DeleteAsync(user!);
        return Deleted<string>();
    }
    #endregion
}