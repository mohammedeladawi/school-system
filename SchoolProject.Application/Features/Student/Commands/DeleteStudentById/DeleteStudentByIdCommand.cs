using SchoolProject.Application.Features.Base.Users.Commands.RequestDTOs;

namespace SchoolProject.Application.Features.Student.Commands.DeleteStudentById;

public record DeleteStudentByIdCommand(int Id) : BaseDeleteUserByIdCommand(Id);