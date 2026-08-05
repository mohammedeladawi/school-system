using SchoolProject.Data.Entities;

namespace SchoolProject.Service.Abstracts;

public interface IInstructorService
{
    Task AddAsync(Instructor instructor);
}
