using AutoMapper;
using SchoolProject.Core.Features.ApplicationUser.Queries.Responses;

namespace SchoolProject.Core.Mapping.ApplicationUser;

public partial class ApplicationUser
{
    public void MapApplicationUserToGetPaginatedApplicationUsersQueryResponse()
    {
        CreateMap<
            Data.Entities.Identities.ApplicationUser,
            GetPaginatedUsersQueryResponse>();
    }

    public void MapApplicationUserToGetApplicationUserByIdQueryResponse()
    {
        CreateMap<
            Data.Entities.Identities.ApplicationUser,
            GetUserByIdQueryResponse>();
    }

}