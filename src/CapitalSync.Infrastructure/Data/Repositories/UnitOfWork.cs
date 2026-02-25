using CapitalSync.Domain.Repositories;

namespace CapitalSync.Infrastructure.Data.Repositories;

public class UnitOfWork : IUnitOfWork
{
    private readonly CapitalSyncDbContext _dbContext;
    public UnitOfWork(CapitalSyncDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task Commit() => await _dbContext.SaveChangesAsync();
}