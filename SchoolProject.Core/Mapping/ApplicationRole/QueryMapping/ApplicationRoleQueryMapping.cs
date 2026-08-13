using AutoMapper;
using SchoolProject.Core.Features.ApplicationRole.Queries.GetAllRoles;
using SchoolProject.Core.Features.ApplicationRole.Queries.GetRoleById;

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