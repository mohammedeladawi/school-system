using SchoolProject.Core.Features.Students.Queries.Responses;
using SchoolProject.Core.Responses;
using SchoolProject.Data.Entities;

namespace SchoolProject.Core.Mapping.Students;

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