using SchoolProject.Application.Interfaces.Bases;
using SchoolProject.Domain.Entities;

namespace SchoolProject.Application.Interfaces.Repositories;

public interface IInstructorRepository : IGenericRepositoryAsync<Instructor>
{
    public Task<bool> DoesNameEnExistAsync(string nameEn, int? excludedId = null);
    public Task<bool> DoesNameArExistAsync(string nameAr, int? excludedId = null);
}
