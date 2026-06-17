using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceManager.Models
{
    public class Budget
    {
        public int Id { get; set; }
        public int LimitAmount { get; set; }
        public int SpentAmount {  get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
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
            int Percent = SpentAmount / LimitAmount * 100;
            return Percent;
        }

    }

}