using AutoMapper;

namespace SchoolProject.Core.Mapping.ApplicationUser;

public partial class ApplicationUser : Profile
{
    public ApplicationUser()
    {
        MapAddApplicationUserCommandToApplicationUser();
        MapApplicationUserToGetPaginatedApplicationUsersQueryResponse();
        MapApplicationUserToGetApplicationUserByIdQueryResponse();
    }
}