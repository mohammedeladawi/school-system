using Microsoft.EntityFrameworkCore;
using SchoolProject.Data.Entities;
using SchoolProject.Infrastructure.Abstracts;
using SchoolProject.Infrastructure.Data;
using SchoolProject.Infrastructure.InfrastructureBases;

namespace SchoolProject.Infrastructure.Repositories;

public class SubjectRepository :
    GenericRepositoryAsync<Subject>,
    ISubjectRepository
{
    private readonly DbSet<Subject> _subjects;

    public SubjectRepository(AppDbContext context) : base(context)
    {
        _subjects = context.Set<Subject>();
    }

}