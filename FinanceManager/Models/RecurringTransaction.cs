using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FinanceManager.Models;

namespace FinanceManager.Models
{
    public class RecurringTransaction  // Повторяющаяся транзакция (подписка)
    {
        public int Id { get; set; }
        public int Amount { get; set; }
        public string Description { get; set; }
        public DateTime NextDate { get; set; }
        public DateTime? EndDate { get; set; }
        public bool IsActive { get; set; } // Активна ли
        public RecurringPeriod RecurringPeriod { get; set; }
        public DateTime GetNextOccurrence()
        {
            if (IsActive == true && EndDate >= NextDate)
            {
                if (RecurringPeriod == RecurringPeriod.Daily) // Ежедневная
                {
                    NextDate = NextDate.AddDays(1);
                }
                if (RecurringPeriod == RecurringPeriod.Monthly) // Ежемесячная
                {
                    NextDate = NextDate.AddMonths(1);
                }
                if (RecurringPeriod == RecurringPeriod.Yearly) // Ежегодная
                {
                    NextDate = NextDate.AddYears(1);
                }
                if (RecurringPeriod == RecurringPeriod.Weekly) // Еженедельная
                {
                    NextDate = NextDate.AddDays(7);
                }
            }
            return NextDate;
        }
    }
}
