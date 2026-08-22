using SchoolProject.Application.Features.Base.Users.Queries.RequestDTOs;

namespace SchoolProject.Application.Features.Student.Queries.GetPaginatedStudents;

public record GetPaginatedStudentsQuery : BaseGetPaginatedUsersQuery<GetPaginatedStudentsQueryResponse>;