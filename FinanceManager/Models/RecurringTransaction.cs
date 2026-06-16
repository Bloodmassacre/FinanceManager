using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceManager.Models
{
    public class RecurringTransaction  // Повторяющаяся транзакция (подписка)
    {
        public int Id { get; set; }
        public int Amount { get; set; }
        public string Description { get; set; }
        //public RecurringPeriod Period { get; set; }
        public DateTime NextDate { get; set; }
        public DateTime? EndDate { get; set; }
        public bool IsActive { get; set; } // Активна ли
    }
}
