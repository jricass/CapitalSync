using CapitalSync.Domain.Entities;
using CapitalSync.Domain.Repositories.Users;
using Microsoft.EntityFrameworkCore;

namespace CapitalSync.Infrastructure.Data.Repositories;

public class UserRepository : IUserRepository
{
    private readonly CapitalSyncDbContext _dbContext;
    public UserRepository(CapitalSyncDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    public async Task Add(User user)
    {
        await _dbContext.Users.AddAsync(user);
    }

    public async Task Delete(User user)
    {
        var userToRemove = await _dbContext.Users.FindAsync(user.Id);
        _dbContext.Users.Remove(userToRemove!);
    }

    public async Task<bool> ExistActiveUserWithEmail(string email)
    {
        return await _dbContext.Users.AnyAsync(user => user.Email.Equals(email) && user.IsActive);
    }

    public async Task<User> GetById(Guid id)
    {
        return await _dbContext.Users.FirstAsync(user => user.Id == id);
    }

    public async Task<User?> GetUserByEmail(string email)
    {
        return await _dbContext.Users.AsNoTracking().FirstOrDefaultAsync(user => user.Email.Equals(email) && user.IsActive);
    }

    public void Update(User user)
    {
        _dbContext.Users.Update(user);
    }
}