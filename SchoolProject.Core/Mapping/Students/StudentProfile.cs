using AutoMapper;
namespace SchoolProject.Core.Mapping.Students;


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
