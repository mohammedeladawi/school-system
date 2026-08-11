using Microsoft.EntityFrameworkCore.Storage;

namespace SchoolProject.Core.Interfaces.Bases;

public interface IGenericRepositoryAsync<T> where T : class
{
    Task DeleteRangeAsync(ICollection<T> entities);
    Task<T> GetByIdAsync(int id);
    Task SaveChangesAsync();
    IDbContextTransaction BeginTransaction();
    void Commit();
    void RollBack();
    IQueryable<T> GetTableNoTracking();
    IQueryable<T> GetTableAsTracking();
    Task<T> AddAsync(T entity);
    Task AddRangeAsync(ICollection<T> entities);
    Task UpdateAsync(T entity);
    Task UpdateRangeAsync(ICollection<T> entities);
    Task DeleteAsync(T entity);
    Task<int> GetTotalCountAsync();
    Task<bool> DoesExistByIdAsync(int id);
}
