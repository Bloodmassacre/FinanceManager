using Avalonia;
using FinanceManager.Data;
using FinanceManager.Enums;
using FinanceManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceManager.Repositories
{
    public class RecurringTransactionRepository : BaseRepository<RecurringTransaction>
    {
        private readonly Database _db = new Database();
        public RecurringTransactionRepository()
        {

        }

        public RecurringTransaction AddRecurringTransaction(int userId, int amount, string description, RecurringPeriod recurringPeriod, DateTime endDate)
        {
            var recurringTransaction = new RecurringTransaction()
            {
                UserId = userId,
                Amount = amount,
                Description = description,
                RecurringPeriod = recurringPeriod,
                EndDate = DateTime.SpecifyKind(endDate, DateTimeKind.Utc),
                IsActive = true

            };
            recurringTransaction.NextDate = recurringTransaction.GetNextOccurrence();
            _db.RecurringTransactions.Add(recurringTransaction);
            _db.SaveChanges();
            return recurringTransaction;
        }
        public void DeleteRecurringTransaction(RecurringTransaction recurringTransaction)
        {
            _db.RecurringTransactions.Remove(recurringTransaction);
            _db.SaveChanges();
        }
        public RecurringTransaction ChangeStatusRecurringTransaction(RecurringTransaction recurringTransaction)
        {
            if (recurringTransaction.NextDate > recurringTransaction.EndDate)
            {
                recurringTransaction.IsActive = false;
                _db.Update(recurringTransaction);
                _db.SaveChanges();
            }
            return recurringTransaction;
        }
        public void RecurringPay(int userId)
        {
            var recurringTransactions = _db.RecurringTransactions
                .Where(x => x.UserId == userId && x.IsActive == true && x.NextDate <= DateTime.UtcNow)
                .ToList();
            foreach (var recurring in recurringTransactions)
            {
                RecurringPayProcess(recurring, DateTime.UtcNow);
            }
        }
        public void RecurringPayProcess(RecurringTransaction recurringTransaction, DateTime dateTime)
        {
            if (recurringTransaction.EndDate.HasValue && recurringTransaction.EndDate.Value <= dateTime)
            {
                recurringTransaction.IsActive = false;
                _db.Update(recurringTransaction);
                _db.SaveChanges();
                return;
            }
            var category = _db.Categories.FirstOrDefault(x => x.Name == "Подписки");
            if (category == null)
            {
                category = new Category
                {
                    Name = "Подписки",
                    Icon = "🔁",
                    TransactionType = TransactionType.Expense,
                    IsDefault = true,
                    Color = "Green"
                };
                _db.Categories.Add(category);
                _db.SaveChanges();
            }
            var transaction = new Transaction
            {
                UserId = recurringTransaction.UserId,
                Amount = recurringTransaction.Amount,
                Description = $"Подписка {recurringTransaction.Description}",
                Date = dateTime,
                transactionType = TransactionType.Expense,
                CategoryId = category.Id
            };
            _db.Transactions.Add(transaction);
            var user = _db.Users.Find(recurringTransaction.UserId);
            if (user != null)
            {
                user.Balance -= recurringTransaction.Amount;
            }
            recurringTransaction.NextDate = recurringTransaction.GetNextOccurrence();
            _db.SaveChanges();
        }
    }
}