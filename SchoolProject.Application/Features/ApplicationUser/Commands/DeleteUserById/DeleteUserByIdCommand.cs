using MediatR;
using SchoolProject.Application.Bases;
using SchoolProject.Application.Features.Base.Users.Commands.RequestDTOs;

namespace SchoolProject.Application.Features.ApplicationUser.Commands.DeleteUserById;

public record DeleteUserByIdCommand(int Id) : BaseDeleteUserByIdCommand(Id);