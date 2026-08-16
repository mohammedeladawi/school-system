using Microsoft.EntityFrameworkCore.Storage;

namespace SchoolProject.Application.Interfaces.Bases;

public interface IUnitOfWork
{
    Task<int> SaveChangesAsync();
    Task BeginTransactionAsync();
    Task CommitAsync();
    Task RollbackAsync();
}
