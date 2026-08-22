using AutoMapper;
using MediatR;
using Microsoft.Extensions.Localization;
using SchoolProject.Application.Bases;
using SchoolProject.Application.Interfaces.IdentityServices;
using SchoolProject.Application.Resources;

namespace SchoolProject.Application.Features.ApplicationUser.Queries.GetUserById;

public class GetUserByIdHandler :
    BaseGetUserByIdHandler<
        GetUserByIdQuery,
        GetUserByIdQueryResponse,
        IUserManager,
        Domain.Entities.Identities.ApplicationUser>
{
    #region Constructors
    public GetUserByIdHandler(
        IUserManager userManager,
        IMapper mapper,
        IStringLocalizer<SharedResource> localizer) : base(userManager, mapper, localizer)
    {
    }
    #endregion
}