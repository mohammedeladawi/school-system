using System.Linq.Expressions;

namespace SchoolProject.Core.Interfaces.Bases;

public interface IGenericRepositoryAsync<T> where T : class
{
    Task<T?> GetByIdAsync(
        int id,
        Expression<Func<T, object>>[]? includes = null,
        bool asNoTracking = true);

    Task AddAsync(T entity);
    Task UpdateAsync(T entity);
    Task DeleteAsync(T entity);
    Task<int> GetTotalCountAsync();
    Task<bool> DoesExistByIdAsync(int id);
    IQueryable<T> GetTableNoTracking();

    Task<List<T>> GetAllAsync(
           Expression<Func<T, object>>[]? includes = null,
           Expression<Func<T, bool>>? filter = null,
           bool asNoTracking = true);

    Task<List<T>> GetPaginatedListAsync(
        int pageNumber = 1,
        int pageSize = 10,
         Expression<Func<T, object>>[]? includes = null,
         Expression<Func<T, bool>>? filter = null,
         bool asNoTracking = true);

}
