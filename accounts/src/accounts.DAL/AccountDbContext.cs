using Microsoft.EntityFrameworkCore;
using accounts.DAL.models;

namespace accounts.DAL
{
    public class AccountDbContext : DbContext
    {
        public virtual DbSet<Account> Accounts { get; set; }
        public virtual DbSet<Customer> Customers { get; set; }
        public virtual DbSet<Transaction> Transactions { get; set; }
        public virtual DbSet<TransactionType> TransactionTypes { get; set; }

        public AccountDbContext()
        {

        }

        public AccountDbContext(DbContextOptions<AccountDbContext> options) : base(options)
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            base.OnConfiguring(options);
        }
        

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<TransactionType>().HasData(new TransactionType { Id = TransactionTypeEnum.Deposit, Name = "Deposit" });
            builder.Entity<TransactionType>().HasData(new TransactionType { Id = TransactionTypeEnum.Transfer, Name = "Transfer" });
            builder.Entity<TransactionType>().HasData(new TransactionType { Id = TransactionTypeEnum.Receive, Name = "Received" });
        }

    }
}
