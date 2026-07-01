using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceManager.Models
{
    public class Budget
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int LimitAmount { get; set; }
        public int SpentAmount { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string Status { get; set; }
        public bool IsOverBudget()
        {
            if (LimitAmount < SpentAmount)
            {

                return true;
            }
            else
            {
                return false;
            }
        }
        public int GetRemaining()
        {
            int Remaining = LimitAmount - SpentAmount;
            return Remaining;
        }
        public int GetProgressPercent()
        {
            if (LimitAmount == 0) return 0;
            int Percent = (int)Math.Round((double)SpentAmount / LimitAmount * 100);
            return Percent;
        }
        public int GetAvailablePercent()
        {
            return 100 - GetProgressPercent();
        }
    }

}