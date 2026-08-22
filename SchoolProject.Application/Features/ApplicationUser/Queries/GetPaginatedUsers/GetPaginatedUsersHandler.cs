using AutoMapper;
using MediatR;
using Microsoft.Extensions.Localization;
using SchoolProject.Application.Bases;
using SchoolProject.Application.Features.Base.Users.Queries.Handlers;
using SchoolProject.Application.Interfaces.IdentityServices;
using SchoolProject.Application.Resources;

namespace SchoolProject.Application.Features.ApplicationUser.Queries.GetPaginatedUsers;

public class GetPaginatedUsersHandler :
    BaseGetPaginatedUsersHandler<
        GetPaginatedUsersQuery,
        GetPaginatedUsersQueryResponse,
        IUserManager,
        Domain.Entities.Identities.ApplicationUser>
{

    #region Constructors
    public GetPaginatedUsersHandler(
        IMapper mapper,
        IStringLocalizer<SharedResource> localizer,
        IUserManager userManager) : base(mapper, localizer, userManager)
    {

    }
    #endregion

}