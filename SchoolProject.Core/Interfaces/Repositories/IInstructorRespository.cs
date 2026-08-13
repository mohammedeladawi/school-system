using SchoolProject.Core.Interfaces.Bases;
using SchoolProject.Data.Entities;

namespace SchoolProject.Core.Interfaces.Repositories;

public interface IInstructorRepository : IGenericRepositoryAsync<Instructor>
{
    public Task<bool> DoesNameEnExistAsync(string nameEn, int? excludedId = null);
    public Task<bool> DoesNameArExistAsync(string nameAr, int? excludedId = null);
}
