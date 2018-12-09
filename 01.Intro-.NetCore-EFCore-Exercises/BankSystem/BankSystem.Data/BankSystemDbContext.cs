using System;
using BankSystem.Models;
using Microsoft.EntityFrameworkCore;

namespace BankSystem.Data
{
    public class BankSystemDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<CheckingAccout> CheckingAccouts { get; set; }
        public DbSet<SavingAccount> SavingAccounts { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder builder)
        {
            if (!builder.IsConfigured)
            {
                builder.UseSqlServer(Configuration.ConnectionString);
            }
            base.OnConfiguring(builder);
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder
                .Entity<User>()
                .HasMany(u => u.CheckingAccouts)
                .WithOne(ca => ca.User)
                .HasForeignKey(ca => ca.UserId);

            builder
                .Entity<User>()
                .HasMany(u => u.SavingAccounts)
                .WithOne(ca => ca.User)
                .HasForeignKey(ca => ca.UserId);



            base.OnModelCreating(builder);
        }
    }
}
