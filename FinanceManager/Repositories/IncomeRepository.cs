using FinanceManager.Models;
using FinanceManager.Scripts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FinanceManager.Enums;
using FinanceManager.Data;

namespace FinanceManager.Repositories
{
    public class IncomeRepository : BaseRepository<Income>
    {
        private readonly Database _db = new Database();
        public IncomeRepository()
        {

        }
        public Income AddIncome(int amount, string description)
        {
            var income = new Income()
            {
                Amount = amount,
                Description = description,
                Date = DateTime.Now,
                transactionType = TransactionType.Income
            };
            //_db.Income.Add(income);
            _db.SaveChanges();
            return income;
        }
    }
}
