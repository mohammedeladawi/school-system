using SchoolProject.Core.Features.Students.Queries.Responses;
using SchoolProject.Data.Entities;

namespace SchoolProject.Core.Mapping.Students;

public partial class StudentProfile
{
    private void MapStudentToStudentDtoForListMapping()
    {
        CreateMap<Student, StudentDtoForList>()
            .ForMember(dest => dest.DepartmentName,
                opt => opt.MapFrom(src => src.Department.Name ?? string.Empty));

    }
}