using CapitalSync.Domain.Security.Cryptography;
using BC = BCrypt.Net.BCrypt;

namespace CapitalSync.Infrastructure.Security.Cryptography;

public class BCrypt : IPasswordEncrypt
{
    // Recebe uma senha em texto puro e usa 'BC.HashPassword' para gerar um hash seguro.
    public string Encrypt(string password)
    {
        string passwordHash = BC.HashPassword(password);

        // Retorna o hash gerado
        return passwordHash;
    }

    public bool Verify(string password, string passwordHash) => BC.Verify(password, passwordHash);
}

/*
    Essa classe serve para criptografar senhas e verificar senhas usando o algoritmo BCrypt.
    Usada para garantir segurança ao armazenar e validar senhas de usuários.
*/