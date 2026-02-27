using CapitalSync.Domain.Repositories.Users;
using CapitalSync.Domain.Security.Cryptography;
using CapitalSync.Domain.Security.Tokens;
using CapitalSync.Domain.Services.LoggedUser;
using CapitalSync.Infrastructure.Data;
using CapitalSync.Infrastructure.Data.Repositories;
using CapitalSync.Infrastructure.Security.Tokens;
using CapitalSync.Infrastructure.Services.LoggedUser;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CapitalSync.Infrastructure;

public static class DependencyInjectionExtension
{
    public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        // Adicionando Criptografia
        services.AddScoped<IPasswordEncrypt, Security.Cryptography.BCrypt>();
        // Usuário Logado
        services.AddScoped<ILoggedUser, LoggedUser>();

        AddToken(services, configuration);
        AddDbContext(services, configuration);
        AddRepositories(services);
    }

    private static void AddToken(IServiceCollection services, IConfiguration configuration)
    {
        var expirationTimeMinutes = uint.Parse(configuration["Settings:Jwt:ExpiresMinutes"]!);
        var signingKey = configuration["Settings:Jwt:SigningKey"]!;

        services.AddScoped<IAccessTokenGenerator>(config => new JwtTokenGenerator(expirationTimeMinutes, signingKey!));
    }

    private static void AddDbContext(IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("DefaultConnection");

        services.AddDbContext<CapitalSyncDbContext>(config => config.UseNpgsql(connectionString));
    }

    private static void AddRepositories(IServiceCollection services)
    {
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<Domain.Repositories.IUnitOfWork, UnitOfWork>();
    }
}