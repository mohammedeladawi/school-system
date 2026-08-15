using AutoMapper;
using SchoolProject.Application.Features.ApplicationRole.Queries.GetAllRoles;
using SchoolProject.Application.Features.ApplicationRole.Queries.GetRoleById;

namespace SchoolProject.Application.Mapping.ApplicationRole;

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