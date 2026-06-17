using FinanceManager.Enums;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace FinanceManager.Models
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Icon { get; set; }
        public string Color { get; set; }
        public bool IsDefault { get; set; } // Основная ли категория
        public TransactionType TransactionType { get; set; }
        public List<Income> Incoming = new List<Income>();
        public List<Expense> Expensing = new List<Expense>();
    }
}
