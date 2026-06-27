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
    public class ExpenseRepository : BaseRepository<Expense>
    {
        private readonly Database _db = new Database();
        public ExpenseRepository()
        {

        }
        public Expense AddExpense(int amount, string description, int categoryId)
        {
            var expense = new Expense()
            {
                Amount = amount,
                Description = description,
                Date = DateTime.UtcNow,
                transactionType = TransactionType.Expense,
                CategoryId = categoryId
            };
            _db.Transactions.Add(expense);
            _db.SaveChanges();
            return expense;
        }
    }
}
