using AutoMapper;
using SchoolProject.Application.Features.ApplicationUser.Commands.EditUser;
using SchoolProject.Application.Features.Authentication.Commands.Register;

namespace SchoolProject.Application.Mapping.ApplicationUser;

public partial class ApplicationUser
{
    public void MapAddApplicationUserCommandToApplicationUser()
    {
        CreateMap<RegisterCommand, Domain.Entities.Identities.ApplicationUser>();
    }
    public void MapEditpplicationUserCommandToApplicationUser()
    {
        CreateMap<EditUserCommand, Domain.Entities.Identities.ApplicationUser>();
    }
}