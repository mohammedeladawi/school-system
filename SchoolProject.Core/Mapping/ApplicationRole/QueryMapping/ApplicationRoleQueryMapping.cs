using AutoMapper;
using SchoolProject.Core.Features.ApplicationRole.Queries.Responses;
using SchoolProject.Core.Features.ApplicationUser.Queries.Responses;

namespace SchoolProject.Core.Mapping.ApplicationRole;

public partial class ApplicationRole
{
    public void MapApplicationRoleToGetAllRolesQueryResponse()
    {
        CreateMap<Data.Entities.Identities.ApplicationRole, GetAllRolesQueryResponse>();
    }

    public void MapApplicationRoleToGetRoleByIdQueryResponse()
    {
        CreateMap<Data.Entities.Identities.ApplicationRole, GetRoleByIdQueryResponse>();
    }
}