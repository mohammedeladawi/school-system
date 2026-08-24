using AutoMapper;
namespace SchoolProject.Application.Mapping.Students;


public partial class StudentProfile : Profile
{
    public StudentProfile()
    {
        MapRegisterStudentCommandToStudent();
        MapEditStudentCommandToStudent();
        MapStudentToGetAllStudentsQueryResponse();
        MapStudentToGetStudentByIdQueryResponse();
        MapStudentToGetPaginatedStudentsQueryResponse();
    }


}
