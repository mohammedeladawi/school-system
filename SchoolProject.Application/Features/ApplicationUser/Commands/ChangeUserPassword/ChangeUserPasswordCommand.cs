using MediatR;
using SchoolProject.Application.Bases;
using SchoolProject.Application.Features.Base.Users.Commands.RequestDTOs;

namespace SchoolProject.Application.Features.ApplicationUser.Commands.ChangeUserPassword;

public record ChangeUserPasswordCommand : BaseChangePasswordCommand;