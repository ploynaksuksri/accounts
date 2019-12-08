using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using accounts.DAL.models;

namespace accounts.DAL
{
    public class AccountDbContext : DbContext
    {
        public virtual DbSet<Account> Accounts { get; set; }
        public virtual DbSet<Customer> Customers { get; set; }
        public virtual DbSet<Transaction> Transactions { get; set; }

        public AccountDbContext() : base()
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {     
            options.UseSqlite("Data Source=accounts.db");
        }

    }
}
