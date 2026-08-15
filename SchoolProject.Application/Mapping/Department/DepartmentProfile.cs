using AutoMapper;

namespace SchoolProject.Application.Mapping.Department;

public partial class DepartmentProfile : Profile
{
    public DepartmentProfile()
    {
        MapDepartmentToGetDepartmentByIdQueryResponse();
        MapDepartmentStudentsCountViewToGetDepartmentStudentsCountQueryResponse();
    }
}