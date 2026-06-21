using FinanceManager.Models;
using Microsoft.EntityFrameworkCore;
using System;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace FinanceManager.Data
{
    public class Database : DbContext
    {

        // Запуск: "C:\Program Files\PostgreSQL\18\bin\pg_ctl.exe" start -D "C:\Program Files\PostgreSQL\18\data"

        // Создание 
        // cd C:\Users\Jora\source\repos\Bloodmassacre\FinanceManager\FinanceManager
        // dotnet ef migrations add InitialCreate
        public DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=FinanceDb;Username=postgres;Password=123456");
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}