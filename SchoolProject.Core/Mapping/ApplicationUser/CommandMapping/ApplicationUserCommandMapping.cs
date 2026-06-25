using AutoMapper;
using SchoolProject.Core.Features.ApplicationUser.Commands.Models;

namespace SchoolProject.Core.Mapping.ApplicationUser;

public partial class ApplicationUser
{
    public void MapAddApplicationUserCommandToApplicationUser()
    {
        CreateMap<AddUserCommand, Data.Entities.Identities.ApplicationUser>();
    }
    public void MapEditpplicationUserCommandToApplicationUser()
    {
        CreateMap<EditUserCommand, Data.Entities.Identities.ApplicationUser>();
    }
}