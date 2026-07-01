using FinanceManager.Models;
using Microsoft.EntityFrameworkCore;
using System;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace FinanceManager.Data
{
    public class Database : DbContext
    {

        // cd C:\Program Files\PostgreSQL\18\bin
        // Запуск: "C:\Program Files\PostgreSQL\18\bin\pg_ctl.exe" start -D "C:\Program Files\PostgreSQL\18\data"
        // Выключение: "C:\Program Files\PostgreSQL\18\bin\pg_ctl.exe" stop -D "C:\Program Files\PostgreSQL\18\data"
        // Перезапуск: "C:\Program Files\PostgreSQL\18\bin\pg_ctl.exe" restart -D "C:\Program Files\PostgreSQL\18\data"
        // Статус: "C:\Program Files\PostgreSQL\18\bin\pg_ctl.exe" status -D "C:\Program Files\PostgreSQL\18\data"
        // Убрать процессы с Postgre: taskkill /IM postgres.exe /F

        // Создание 
        // cd C:\Users\Jora\source\repos\Bloodmassacre\FinanceManager\FinanceManager
        // dotnet ef migrations add Initialreate

        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Budget> Budgets { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<RecurringTransaction> RecurringTransactions { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var host = Environment.GetEnvironmentVariable("DB_HOST") ?? "localhost";
            var connectionString = $"Host={host};Port=5432;Database=FinanceDb;Username=postgres;Password=123456";

            optionsBuilder.UseNpgsql(connectionString);
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Transaction>()
             .Property(t => t.CategoryId)
             .HasColumnName("CategoryId");
        }
    }
}