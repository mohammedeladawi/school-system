namespace SchoolProject.Service.ServiceBases;

public interface IGenericService<T>
{
    public Task<T?> GetByIdAsync(int id);
    public Task<List<T>> GetAllAsync();
    public Task AddAsync(T entity);
    public Task UpdateAsync(T entity);
    public Task DeleteByIdAsync(int id);
    public Task<bool> IsExistByIdAsync(int id);
}