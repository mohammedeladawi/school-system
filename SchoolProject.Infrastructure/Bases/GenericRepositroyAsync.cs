using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using SchoolProject.Core.Interfaces.Bases;
using SchoolProject.Infrastructure.Data;

namespace SchoolProject.Infrastructure.InfrastructureBases;

public class GenericRepositoryAsync<T> :
    IGenericRepositoryAsync<T> where T : class
{
    #region Protected Fields
    protected readonly AppDbContext _dbContext;
    #endregion

    #region Constructor(s)
    public GenericRepositoryAsync(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    #endregion

    #region Public Methods
    public virtual async Task<T?> GetByIdAsync(
        int id,
        Expression<Func<T, bool>>? filter = null,
        Expression<Func<T, object>>[]? includes = null)
    {
        IQueryable<T> query = _dbContext.Set<T>();

        if (filter != null)
            query = query.Where(filter);


        if (includes != null)
        {
            foreach (var include in includes)
            {
                query = query.Include(include);
            }
        }

        return await query.FirstOrDefaultAsync(e => EF.Property<int>(e, "Id") == id);
    }

    public IQueryable<T> GetTableNoTracking()
    {
        return _dbContext.Set<T>().AsNoTracking().AsQueryable();
    }

    public virtual async Task AddAsync(T entity)
    {
        await _dbContext.Set<T>().AddAsync(entity);
    }

    public virtual async Task UpdateAsync(T entity)
    {
        _dbContext.Set<T>().Update(entity);
    }

    public virtual async Task DeleteAsync(T entity)
    {
        _dbContext.Set<T>().Remove(entity);
    }

    public virtual async Task<int> GetTotalCountAsync()
    {
        return await _dbContext.Set<T>().CountAsync();
    }

    public async Task<bool> DoesExistByIdAsync(int id)
    {
        return await GetTableNoTracking().AnyAsync(e => EF.Property<int>(e, "Id") == id);
    }

    #endregion
}