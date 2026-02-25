using CapitalSync.Domain.Enums;

namespace CapitalSync.Application.DTOs.Users.Responses;

public class ResponseUserJson
{
    public Guid Id { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string UserRole { get; set; } = UserRoles.CUSTOMER;
    public bool IsActive { get; set; }
}