using SchoolProject.Core.Features.Department.Queries.Responses;

namespace SchoolProject.Core.Mapping.Department;

public partial class DepartmentProfile
{
    public void MapDepartmentToGetDepartmentByIdQueryResponse()
    {
        CreateMap<Data.Entities.Department, GetDepartmentByIdQueryResponse>()
            .ForMember(
                dest => dest.ManagerName,
                opt => opt.MapFrom(src => src.Manager != null ? $"{src.Manager.Name}" : string.Empty));

        CreateMap<Data.Entities.Instructor, InstructorInDepartmentDto>();
        CreateMap<Data.Entities.Subject, SubjectInDepartmentDto>();
        CreateMap<Data.Entities.Student, StudentInDepartmentDto>();
    }
}