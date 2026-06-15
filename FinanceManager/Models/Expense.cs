using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceManager.Models
{
    public class Expense // Траты
    {
        public int Id { get; set; }
        public decimal Amount { get; set; }
        public string Description { get; set; }
        public DateTime Date {  get; set; }
        public string Payee { get; set; } // Получатель платежа (???)
        public bool IsReccuring { get; set; }
    }
}
