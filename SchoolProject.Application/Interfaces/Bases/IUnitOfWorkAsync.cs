using Microsoft.EntityFrameworkCore.Storage;

namespace SchoolProject.Application.Interfaces.Bases;

public interface IUnitOfWork : IDisposable
{
    Task<int> SaveChangesAsync();
    Task<IDbContextTransaction> BeginTransactionAsync();
    Task CommitAsync();
    Task RollbackAsync();
}
