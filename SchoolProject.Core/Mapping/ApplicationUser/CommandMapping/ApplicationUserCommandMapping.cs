using AutoMapper;
using SchoolProject.Core.Features.ApplicationUser.Commands.EditUser;
using SchoolProject.Core.Features.Authentication.Commands.Register;

namespace SchoolProject.Core.Mapping.ApplicationUser;

public partial class ApplicationUser
{
    public void MapAddApplicationUserCommandToApplicationUser()
    {
        CreateMap<RegisterCommand, Data.Entities.Identities.ApplicationUser>();
    }
    public void MapEditpplicationUserCommandToApplicationUser()
    {
        CreateMap<EditUserCommand, Data.Entities.Identities.ApplicationUser>();
    }
}