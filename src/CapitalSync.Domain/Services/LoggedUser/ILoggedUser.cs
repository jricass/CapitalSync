using CapitalSync.Domain.Entities;

namespace CapitalSync.Domain.Services.LoggedUser;

public interface ILoggedUser
{
    Task<User> Get();
}