using SchoolProject.Application.Interfaces.Bases;
using SchoolProject.Domain.Entities;

namespace SchoolProject.Application.Interfaces.Repositories;

public interface IStudentManager : IGenericIdentityUserManagerAsync<Student>;