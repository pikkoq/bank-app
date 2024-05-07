
using Azure;
using Bank_app_api.Models;

namespace Bank_app_api.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<Account>()
                .Property(a => a.AccountNumber)
                .HasMaxLength(20);

            modelBuilder.Entity<Transaction>()
                .HasKey(o => o.TransactionId);

            modelBuilder.Entity<Transaction>()
                .Property(o => o.FromAccountNumber)
                .HasMaxLength(20);

            modelBuilder.Entity<Transaction>()
                .Property(o => o.ToAccountNumber)
                .HasMaxLength(20);

            //relacje
            modelBuilder.Entity<Account>()
                .HasOne(a => a.Owner)
                .WithMany(u => u.Accounts)
                .HasForeignKey(a => a.OwnerId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Transaction>()
                .HasOne(a => a.FromAccount)
                .WithMany(u => u.FromTransfers)
                .HasForeignKey(a => a.FromAccountNumber)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Transaction>()
                .HasOne(a => a.ToAccount)
                .WithMany(u => u.ToTransfers)
                .HasForeignKey(a => a.ToAccountNumber)
                .OnDelete(DeleteBehavior.Restrict);
        }

        public DbSet<User> Users => Set<User>();
        public DbSet<Account> Accounts => Set<Account>();
        public DbSet<Transaction> Transactions => Set<Transaction>();

    }
}
