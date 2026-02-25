using CapitalSync.Domain.Enums;

namespace CapitalSync.Domain.Entities;

public class Transaction
{
    public Guid Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string? Description { get; set; }
    public DateTime Date { get; set; }
    public decimal Amount { get; set; }
    public TransactionType TransactionType { get; set; }
    public PaymentType PaymentType { get; set; }
    public Guid AccountId { get; set; }
}