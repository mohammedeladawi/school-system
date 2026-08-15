using SchoolProject.Application.Features.Department.Queries.GetDepartmentById;
using SchoolProject.Application.Features.Department.Queries.GetDepartmentStudentsCount;

namespace SchoolProject.Application.Mapping.Department;

public partial class DepartmentProfile
{
    public void MapDepartmentToGetDepartmentByIdQueryResponse()
    {
        CreateMap<Domain.Entities.Department, GetDepartmentByIdQueryResponse>()
            .ForMember(
                dest => dest.ManagerName,
                opt => opt.MapFrom(src => src.Manager != null ? $"{src.Manager.Name}" : string.Empty));

        CreateMap<Domain.Entities.Instructor, InstructorInDepartmentDto>();
        CreateMap<Domain.Entities.Subject, SubjectInDepartmentDto>();
        CreateMap<Domain.Entities.Student, StudentInDepartmentDto>();
    }

    public void MapDepartmentStudentsCountViewToGetDepartmentStudentsCountQueryResponse()
    {
        CreateMap<Domain.Entities.Views.DepartmentStudentsCountView, GetDepartmentStudentsCountQueryResponse>();

    }
}

