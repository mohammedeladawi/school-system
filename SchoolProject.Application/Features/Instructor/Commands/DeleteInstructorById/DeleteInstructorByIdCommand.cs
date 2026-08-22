using MediatR;
using SchoolProject.Application.Bases;
using SchoolProject.Application.Features.Base.Users.Commands.RequestDTOs;

namespace SchoolProject.Application.Features.ApplicationUser.Commands.DeleteInstructorById;

public record DeleteInstructorByIdCommand(int Id) : BaseDeleteUserByIdCommand(Id);