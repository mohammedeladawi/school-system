using Microsoft.EntityFrameworkCore.Storage;
using SchoolProject.Application.Interfaces.Bases;
using SchoolProject.Infrastructure.Data;

namespace SchoolProject.Infrastructure.Bases;

public class UnitOfWork : IUnitOfWork
{
    private readonly AppDbContext _dbContext;
    private IDbContextTransaction? _transaction;

    public UnitOfWork(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<int> SaveChangesAsync()
    {
        return await _dbContext.SaveChangesAsync();
    }

    public async Task BeginTransactionAsync()
    {
        _transaction =
            await _dbContext.Database.BeginTransactionAsync();

    }

    public async Task CommitAsync()
    {
        if (_transaction is null)
            throw new InvalidOperationException("No active transaction.");

        await SaveChangesAsync();
        await _dbContext.Database.CommitTransactionAsync();
        await _transaction.DisposeAsync();
        _transaction = null;
    }

    public async Task RollbackAsync()
    {
        if (_transaction is null)
            return;

        await _dbContext.Database.RollbackTransactionAsync();
        await _transaction.DisposeAsync();
        _transaction = null;
    }
}