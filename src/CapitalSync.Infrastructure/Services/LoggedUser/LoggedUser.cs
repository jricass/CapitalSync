using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using CapitalSync.Domain.Entities;
using CapitalSync.Domain.Security.Tokens;
using CapitalSync.Domain.Services.LoggedUser;
using CapitalSync.Exception;
using CapitalSync.Exception.ExceptionsBase;
using CapitalSync.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace CapitalSync.Infrastructure.Services.LoggedUser;

public class LoggedUser : ILoggedUser
{
    private readonly CapitalSyncDbContext _dbContext;
    private readonly ITokenProvider _tokenProvider;

    public LoggedUser(CapitalSyncDbContext dbContext, ITokenProvider tokenProvider)
    {
        _dbContext = dbContext;
        _tokenProvider = tokenProvider;
    }

    public async Task<User> Get()
    {
        string token = _tokenProvider.TokenOnRequest();

        var tokenHandler = new JwtSecurityTokenHandler();

        var jwtSecurityToken = tokenHandler.ReadJwtToken(token);

        var identifier = jwtSecurityToken.Claims.First(claim => claim.Type == ClaimTypes.Sid).Value;

        var user = await _dbContext
            .Users
            .AsNoTracking()
            .FirstOrDefaultAsync(user => user.Id == Guid.Parse(identifier));
        
        if (user is null || !user.IsActive)
        {
            throw new UserInactiveException();
        }

        return user;
    }
}