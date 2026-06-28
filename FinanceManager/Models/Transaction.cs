using FinanceManager.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceManager.Models
{
    public class Transaction
    {
        public int Id { get; set; }
        public int Amount { get; set; }
        public string Description { get; set; }
        public DateTime Date { get; set; }
        public TransactionType transactionType { get; set; }
        public int CategoryId { get; set; }
        public Category Category { get; set; }
        public string GetFormattedAmount()
        {
            return Amount.ToString("C0");
        }
    }

    public class Income : Transaction
    {
        public string Source { get; set; } // Источник
        public Income()
        {
        transactionType = TransactionType.Income;
        }
    }
    public class Expense : Transaction
    {
        public string Payee { get; set; } // Получатель
        public Expense()
        {
            transactionType = TransactionType.Expense;
        }
    }
}
