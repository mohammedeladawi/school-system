using SchoolProject.Core.Interfaces.Bases;
using SchoolProject.Data.Entities;
using StudentProject.Data.Enums;

namespace SchoolProject.Core.Interfaces.Repositories;

public interface IStudentRepository : IGenericRepositoryAsync<Student>
{
    public Task<bool> DoesNameEnExistAsync(string nameEn, int? excludedId = null);
    public Task<bool> DoesNameArExistAsync(string nameAr, int? excludedId = null);
}