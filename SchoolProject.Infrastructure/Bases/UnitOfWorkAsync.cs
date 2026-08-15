using Microsoft.EntityFrameworkCore.Storage;
using SchoolProject.Application.Interfaces.Bases;
using SchoolProject.Infrastructure.Data;

namespace SchoolProject.Infrastructure.Bases;

public class UnitOfWork : IUnitOfWork
{
    private readonly AppDbContext _dbContext;

    public UnitOfWork(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<int> SaveChangesAsync()
    {
        return await _dbContext.SaveChangesAsync();
    }

    public async Task<IDbContextTransaction> BeginTransactionAsync()
    {
        return await _dbContext.Database.BeginTransactionAsync();
    }

    public async Task CommitAsync()
    {
        await _dbContext.Database.CommitTransactionAsync();

    }

    public async Task RollbackAsync()
    {
        await _dbContext.Database.RollbackTransactionAsync();
    }

    public void Dispose()
    {
        _dbContext.Dispose();
    }
}