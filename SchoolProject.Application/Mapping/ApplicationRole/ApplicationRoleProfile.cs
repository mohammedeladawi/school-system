using AutoMapper;

namespace SchoolProject.Application.Mapping.ApplicationRole;

public partial class ApplicationRole : Profile
{
    public ApplicationRole()
    {
        MapApplicationRoleToGetAllRolesQueryResponse();
        MapApplicationRoleToGetRoleByIdQueryResponse();
    }
}