using CapitalSync.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace CapitalSync.Infrastructure.Data;

public class CapitalSyncDbContext : DbContext
{
    public CapitalSyncDbContext(DbContextOptions<CapitalSyncDbContext> options) : base(options) { }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
    }

    public DbSet<User> Users { get; set; }
    public DbSet<Account> Accounts { get; set; }
    public DbSet<Transaction> Transactions { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
    }
}