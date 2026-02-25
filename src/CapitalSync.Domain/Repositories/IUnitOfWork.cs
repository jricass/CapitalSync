namespace CapitalSync.Domain.Repositories;

public interface IUnitOfWork
{
    Task Commit();
}