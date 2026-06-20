using FinanceManager.Models;
using Microsoft.EntityFrameworkCore;
using System;

namespace FinanceManager.Data
{
    public class Database : DbContext
    {
        public DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql("Host=localhost;Database=FinanceDb;Username=postgres;Password=your_password");
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}