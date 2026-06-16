using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceManager.Models
{
    public class Income // Доход
    {
        public int Id { get; set; }
        public int Amount { get; set; }
        public string Description { get; set; }
        public DateTime Date { get; set; }
        public string Source { get; set; } // Источник дохода
        public bool IsRecurring { get; set; } // Повторяется ли доход
    }
}
