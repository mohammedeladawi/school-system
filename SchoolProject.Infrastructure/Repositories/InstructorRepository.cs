using Microsoft.EntityFrameworkCore;
using SchoolProject.Data.Entities;
using SchoolProject.Infrastructure.Abstracts;
using SchoolProject.Infrastructure.Data;
using SchoolProject.Infrastructure.InfrastructureBases;

namespace SchoolProject.Infrastructure.Repositories;

public class InstructorRepository :
    GenericRepositoryAsync<Instructor>,
    IInstructorRepository
{
    private readonly DbSet<Instructor> _instructors;

    public InstructorRepository(AppDbContext context) : base(context)
    {
        _instructors = context.Set<Instructor>();
    }

}
