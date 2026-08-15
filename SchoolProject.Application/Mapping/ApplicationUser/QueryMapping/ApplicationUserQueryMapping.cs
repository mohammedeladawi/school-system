using AutoMapper;
using SchoolProject.Application.Features.ApplicationUser.Queries.GetPaginatedUsers;
using SchoolProject.Application.Features.ApplicationUser.Queries.GetUserById;

namespace SchoolProject.Application.Mapping.ApplicationUser;

public partial class ApplicationUser
{
    public void MapApplicationUserToGetPaginatedApplicationUsersQueryResponse()
    {
        CreateMap<
            Domain.Entities.Identities.ApplicationUser,
            GetPaginatedUsersQueryResponse>();
    }

    public void MapApplicationUserToGetApplicationUserByIdQueryResponse()
    {
        CreateMap<
            Domain.Entities.Identities.ApplicationUser,
            GetUserByIdQueryResponse>();
    }

}