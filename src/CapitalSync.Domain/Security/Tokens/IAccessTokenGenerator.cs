using CapitalSync.Domain.Entities;

namespace CapitalSync.Domain.Security.Tokens;

public interface IAccessTokenGenerator
{
    string Generate(User user);
}