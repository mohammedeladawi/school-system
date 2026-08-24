using SchoolProject.Application.Features.Base.Users.Queries.RequestDTOs;

namespace SchoolProject.Application.Features.Student.Queries.GetStudentById;

public record GetStudentByIdQuery(int Id) : BaseGetUserByIdQuery<GetStudentByIdQueryResponse>(Id);