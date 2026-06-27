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
        public Budget AddBudget(int budgetCount)
        {
            var budget = new Budget()
            {
                LimitAmount = budgetCount,
                SpentAmount = 0,
                StartDate = DateTime.UtcNow
            };
            _db.Budgets.Add(budget);
            _db.SaveChanges();
            return budget;
        }
        public string ChangeStatus()
        {
            var budget = _db.Budgets.FirstOrDefault();
            int percent = budget.GetProgressPercent();
            if (percent > 80)
            {
                budget.Status = "Warning";
            }
            if (budget.IsOverBudget() == true)
            {
                budget.Status = "Exceeded";
            }
            if (budget.EndDate == DateTime.Now)
            {
                budget.Status = "Expired";
            }
            _db.Budgets.Update(budget);
            _db.SaveChanges();
            return budget.Status;
        }
    }
}
