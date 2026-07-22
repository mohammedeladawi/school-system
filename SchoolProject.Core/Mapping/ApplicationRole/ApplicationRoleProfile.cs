using AutoMapper;

namespace SchoolProject.Core.Mapping.ApplicationRole;

public partial class ApplicationRole : Profile
{
    public ApplicationRole()
    {
        MapApplicationRoleToGetAllRolesQueryResponse();
        MapApplicationRoleToGetRoleByIdQueryResponse();
    }
}