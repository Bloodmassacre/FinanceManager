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

        public RecurringTransaction AddRecurringTransaction(int amount, string description, RecurringPeriod recurringPeriod, DateTime nextDate, DateTime endDate)
        {
            var recurringTransaction = new RecurringTransaction()
            {
                Amount = amount,
                Description = description,
                RecurringPeriod = recurringPeriod,
                NextDate = nextDate,
                EndDate = endDate,
                IsActive = true
                
            };
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
        public void RecurringPay()
        {
            var recurringTransactions = _db.RecurringTransactions
                .Where(x => x.IsActive == true && x.NextDate <= DateTime.UtcNow)
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
            var user = _db.Users.FirstOrDefault();
            user.Balance =- recurringTransaction.Amount;
            recurringTransaction.NextDate = recurringTransaction.GetNextOccurrence();
        }
    }
}
