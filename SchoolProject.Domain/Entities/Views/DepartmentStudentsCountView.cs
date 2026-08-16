using SchoolProject.Domain.Helpers;

namespace SchoolProject.Domain.Entities.Views;

public class DepartmentStudentsCountView
{
    public int DepartmentId { get; set; }
    public string DepartmentNameEn { get; set; } = null!;
    public string DepartmentNameAr { get; set; } = null!;
    public string DepartmentName => GeneralLocalizableEntity.LocalizeText(DepartmentNameEn, DepartmentNameAr);
    public int StudentsCount { get; set; }
}
