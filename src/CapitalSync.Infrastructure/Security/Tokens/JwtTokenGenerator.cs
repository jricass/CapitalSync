using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using CapitalSync.Domain.Entities;
using CapitalSync.Domain.Security.Tokens;
using Microsoft.IdentityModel.Tokens;

namespace CapitalSync.Infrastructure.Security.Tokens;

public class JwtTokenGenerator : IAccessTokenGenerator
{
    private readonly uint _expirationTimeMinutes;
    private readonly string _signingKey; // Chave secreta para assinar Token.

    public JwtTokenGenerator(uint expirationTimeMinutes, string signingKey)
    {
        _expirationTimeMinutes = expirationTimeMinutes;
        _signingKey = signingKey;
    }


    public string Generate(User user)
    {
        // Cria uma lista de claims que contém informações do usuário
        var claims = new List<Claim>()
        {
            new Claim(ClaimTypes.Name, $"{user.FirstName} {user.LastName}"),
            new Claim(ClaimTypes.GivenName, user.FirstName),
            new Claim(ClaimTypes.Sid, user.Id.ToString()),
            new Claim(ClaimTypes.Role, user.UserRole)
        };

        // tokenDescriptor: Define como um Token JWT deve ser gerado
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Expires = DateTime.UtcNow.AddMinutes(_expirationTimeMinutes), // Data de expiração
            SigningCredentials = new SigningCredentials(SecurityKey(), SecurityAlgorithms.HmacSha256Signature), // Infos para assinar Token
            Subject = new ClaimsIdentity(claims) // Identidade do usuário, conjunto de informações associadas ao usuário
        };

        var tokenHandler = new JwtSecurityTokenHandler(); // Cria um objeto responsável por gerar e manipular tokens JWT

        var securityToken = tokenHandler.CreateToken(tokenDescriptor); // Usa o método CreateToken a partir do tokenHandler

        return tokenHandler.WriteToken(securityToken);
    }

    private SymmetricSecurityKey SecurityKey()
    {
        var key = Encoding.UTF8.GetBytes(_signingKey);

        return new SymmetricSecurityKey(key);
    }
}