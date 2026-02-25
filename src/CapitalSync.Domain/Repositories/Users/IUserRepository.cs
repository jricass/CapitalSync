using CapitalSync.Domain.Entities;

namespace CapitalSync.Domain.Repositories.Users;

public interface IUserRepository
{
    Task Add(Entities.User user);
    Task Delete(Entities.User user);
    Task<Entities.User> GetById(Guid id);
    void Update(Entities.User user);
    Task<bool> ExistActiveUserWithEmail(string email);
    Task<User?> GetUserByEmail(string email);
}