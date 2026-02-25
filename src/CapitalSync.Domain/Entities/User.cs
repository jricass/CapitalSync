using CapitalSync.Domain.Enums;

namespace CapitalSync.Domain.Entities;

public class User
{
    public Guid Id { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public string UserRole { get; set; } = UserRoles.CUSTOMER;
    public bool IsActive { get; private set; } = true;

    public void Deactivate()
    {
        IsActive = false;
    }
}