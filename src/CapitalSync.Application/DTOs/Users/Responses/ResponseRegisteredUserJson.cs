using CapitalSync.Domain.Enums;

namespace CapitalSync.Application.DTOs.Users.Responses;

public class ResponseRegisteredUserJson
{
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string UserRole { get; set; } = string.Empty;
    public string Token { get; set; } = string.Empty;
}