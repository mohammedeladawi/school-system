using MediatR;
using SchoolProject.Application.Bases;

namespace SchoolProject.Application.Features.ApplicationUser.Commands.DeleteInstructorById;

public record DeleteInstructorByIdCommand(int Id) : IRequest<Response<string>>;