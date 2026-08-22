using MediatR;
using SchoolProject.Application.Bases;
using SchoolProject.Application.Features.Base.Users.Commands.RequestDTOs;

namespace SchoolProject.Application.Features.Instructor.Commands.ChangeInstructorPassword;

public record ChangeInstructorPasswordCommand : BaseChangePasswordCommand;
