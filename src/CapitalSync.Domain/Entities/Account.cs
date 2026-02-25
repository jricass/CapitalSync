using CapitalSync.Domain.Enums;

namespace CapitalSync.Domain.Entities;

public class Account
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public decimal Balance { get; private set; }
    public bool IsActive { get; private set; } = true;
    public Guid UserId { get; set; }

    public void Deactivate()
    {
        IsActive = false;
    }
    
    public void UpdateBalance(decimal amount, TransactionType type)
    {
        if (type == TransactionType.Income)
            Balance += amount;
        else
            Balance -= amount;
    }
}