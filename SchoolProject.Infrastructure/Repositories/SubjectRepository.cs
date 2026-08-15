using Microsoft.EntityFrameworkCore;
using SchoolProject.Application.Interfaces.Repositories;
using SchoolProject.Domain.Entities;
using SchoolProject.Infrastructure.Data;
using SchoolProject.Infrastructure.Bases;

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
        _subjects = context.Subjects;
    }
    #endregion

    #region Public Methods
    // No public methods beyond inherited generic methods.
    #endregion

}