using System.Linq.Expressions;

namespace SchoolProject.Core.Interfaces.Bases;

public interface IGenericRepositoryAsync<T> where T : class
{
    Task<T?> GetByIdAsync(
        int id,
        Expression<Func<T, bool>>? filter = null,
        Expression<Func<T, object>>[]? includes = null);
    Task AddAsync(T entity);
    Task UpdateAsync(T entity);
    Task DeleteAsync(T entity);
    Task<int> GetTotalCountAsync();
    Task<bool> DoesExistByIdAsync(int id);
    IQueryable<T> GetTableNoTracking();
}
