using AutoMapper;
namespace SchoolProject.Application.Mapping.Students;


public partial class StudentProfile : Profile
{
    public StudentProfile()
    {
        MapStudentToGetAllStudentsQueryResponse();
        MapStudentToGetStudentByIdQueryResponse();
        MapAddStudentCommandToStudent();
        MapEditStudentCommandToStudent();
        MapStudentToGetPaginatedStudentsQueryResponse();
    }


}
