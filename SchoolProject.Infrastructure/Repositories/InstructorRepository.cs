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
    #region Private Fields
    private readonly DbSet<Instructor> _instructors;
    #endregion

    #region Constructors
    public InstructorRepository(AppDbContext context) : base(context)
    {
        _instructors = context.Set<Instructor>();
    }
    #endregion

    #region Public Methods
    // No public methods beyond inherited generic methods.
    #endregion
}
