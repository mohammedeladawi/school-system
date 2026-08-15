using AutoMapper;

namespace SchoolProject.Application.Mapping.ApplicationUser;

public partial class ApplicationUser : Profile
{
    public ApplicationUser()
    {
        MapAddApplicationUserCommandToApplicationUser();
        MapApplicationUserToGetPaginatedApplicationUsersQueryResponse();
        MapApplicationUserToGetApplicationUserByIdQueryResponse();
        MapEditpplicationUserCommandToApplicationUser();
    }
}