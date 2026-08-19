using AutoMapper;
using SchoolProject.Application.Features.ApplicationUser.Commands.EditUser;
using SchoolProject.Application.Features.ApplicationUser.Commands.RegisterUser;
using SchoolProject.Application.Features.Authentication.Commands.RegisterOrUpdate;
using SchoolProject.Application.Features.Authentication.Commands.RegisterOrUpdate.Admin;

namespace SchoolProject.Application.Mapping.ApplicationUser;

public partial class ApplicationUser
{
    public void MapAddApplicationUserCommandToApplicationUser()
    {
        CreateMap<RegisterUserCommand, Domain.Entities.Identities.ApplicationUser>()
            .ForMember(dest => dest.ImagePath, opt => opt.Ignore());
    }
    public void MapEditApplicationUserCommandToApplicationUser()
    {
        CreateMap<EditUserCommand, Domain.Entities.Identities.ApplicationUser>()
            .ForMember(dest => dest.ImagePath, opt => opt.Ignore());
    }
}