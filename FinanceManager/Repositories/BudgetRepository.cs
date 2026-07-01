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
    public class BudgetRepository : BaseRepository<Budget>
    {
        private readonly Database _db = new Database();
        public BudgetRepository()
        {

        }
        public Budget AddBudget(int userId, int budgetCount, DateTime enddate)
        {
            var budget = new Budget()
            {
                UserId = userId,
                LimitAmount = budgetCount,
                SpentAmount = 0,
                StartDate = DateTime.UtcNow,
                EndDate = DateTime.SpecifyKind(enddate, DateTimeKind.Utc),
                Status = "Active"
            };
            _db.Budgets.Add(budget);
            _db.SaveChanges();
            return budget;
        }
        public string ChangeStatus(int userId)
        {
            var budget = _db.Budgets.FirstOrDefault(x => x.UserId == userId);
            if (budget == null)
            {
                return "No budget";
            }
            int percent = budget.GetProgressPercent();
            if (budget.EndDate <= DateTime.UtcNow)
            {
                budget.Status = "Expired";
            }
            if (percent >= 100)
            {
                budget.Status = "Exceeded";
            }
            if (percent >= 80 && percent < 100)
            {
                budget.Status = "Warning";
            }
            else
            {
                budget.Status = "Active";
            }
            _db.SaveChanges();
            return budget.Status;
        }
    }
}