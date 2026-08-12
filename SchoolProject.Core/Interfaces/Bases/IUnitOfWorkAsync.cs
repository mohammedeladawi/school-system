using Microsoft.EntityFrameworkCore.Storage;

namespace SchoolProject.Core.Interfaces.Bases;

public interface IUnitOfWorkAsync
{
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    Task<IDbContextTransaction> BeginTransactionAsync();
    Task CommitAsync();
    Task RollbackAsync();
}
