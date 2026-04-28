using SchoolProject.Data.Helpers;

namespace SchoolProject.Data.Entities;

public class Student
{
    public int Id { get; set; }
    public string NameEn { get; set; } = null!;
    public string NameAr { get; set; } = null!;

    // This property is not mapped to the database, it's used to get the localized name based on the current culture
    public string Name => GeneralLocalizableEntity.LocalizeText(NameEn, NameAr);
    public string Address { get; set; } = null!;
    public string Phone { get; set; } = null!;

    public int DepartmentId { get; set; }
    public Department Department { get; set; } = null!;

    public List<Subject> Subjects { get; set; } = new();
}