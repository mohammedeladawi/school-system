using Microsoft.EntityFrameworkCore;
using SchoolProject.Core.Interfaces.Repositories;
using SchoolProject.Data.Entities;
using SchoolProject.Infrastructure.Data;
using SchoolProject.Infrastructure.InfrastructureBases;

namespace SchoolProject.Infrastructure.Repositories;

public class SubjectRepository :
    GenericRepositoryAsync<Subject>,
    ISubjectRepository
{
    #region Private Fields
    private readonly DbSet<Subject> _subjects;
    #endregion

    #region Constructors
    public SubjectRepository(AppDbContext context) : base(context)
    {
        _subjects = context.Set<Subject>();
    }
    #endregion

    #region Public Methods
    // No public methods beyond inherited generic methods.
    #endregion

}