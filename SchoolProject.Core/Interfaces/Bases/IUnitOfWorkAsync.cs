using Microsoft.EntityFrameworkCore.Storage;

namespace SchoolProject.Core.Interfaces.Bases;

public interface IUnitOfWorkAsync
{
    Task<int> SaveChangesAsync();
    Task<IDbContextTransaction> BeginTransactionAsync();
    Task CommitAsync();
    Task RollbackAsync();
}
