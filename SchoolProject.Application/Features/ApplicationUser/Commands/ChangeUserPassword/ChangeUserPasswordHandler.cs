using AutoMapper;
using MediatR;
using Microsoft.Extensions.Localization;
using SchoolProject.Application.Bases;
using SchoolProject.Application.Features.Base.Users.Commands.Handlers;
using SchoolProject.Application.Interfaces.IdentityServices;
using SchoolProject.Application.Resources;

namespace SchoolProject.Application.Features.ApplicationUser.Commands.ChangeUserPassword;

public class ChangeUserPasswordHandler :
    BaseChangePasswordHandler<
        ChangeUserPasswordCommand,
        IUserManager,
        Domain.Entities.Identities.ApplicationUser>
{
    #region Constructors
    public ChangeUserPasswordHandler(
        IStringLocalizer<SharedResource> localizer,
        IMapper mapper,
        IUserManager userManager)
        : base(localizer, mapper, userManager)
    {
    }
    #endregion
}