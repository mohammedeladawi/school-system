using SchoolProject.Application.Features.Student.Queries.GetAllStudents;
using SchoolProject.Application.Features.Student.Queries.GetStudentById;
using SchoolProject.Application.Features.Student.Queries.GetPaginatedStudents;
using SchoolProject.Domain.Entities;

namespace SchoolProject.Application.Mapping.Students;

public partial class StudentProfile
{
    private void MapStudentToGetAllStudentsQueryResponse()
    {
        CreateMap<Student, GetAllStudentsQueryResponse>()
            .ForMember(
                dest => dest.DepartmentName,
                opt => opt.MapFrom(src => src.Department.Name ?? string.Empty));

    }

    private void MapStudentToGetStudentByIdQueryResponse()
    {
        CreateMap<Student, GetStudentByIdQueryResponse>()
           .ForMember(
                dest => dest.DepartmentName,
                opt => opt.MapFrom(src => src.Department.Name ?? string.Empty));
    }

    private void MapStudentToGetPaginatedStudentsQueryResponse()
    {
        CreateMap<Student, GetPaginatedStudentsQueryResponse>()
           .ForMember(
                dest => dest.DepartmentName,
                opt => opt.MapFrom(src => src.Department.Name ?? string.Empty));
    }
}