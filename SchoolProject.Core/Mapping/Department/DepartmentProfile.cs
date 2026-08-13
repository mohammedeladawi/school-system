using AutoMapper;

namespace SchoolProject.Core.Mapping.Department;

public partial class DepartmentProfile : Profile
{
    public DepartmentProfile()
    {
        MapDepartmentToGetDepartmentByIdQueryResponse();
        MapDepartmentStudentsCountViewToGetDepartmentStudentsCountQueryResponse();
    }
}