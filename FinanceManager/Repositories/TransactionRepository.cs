using FinanceManager.Data;
using FinanceManager.Enums;
using FinanceManager.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceManager.Repositories
{
    public class TransactionRepository : BaseRepository<Transaction>
    {
        private readonly Database _db = new Database();
        public TransactionRepository()
        {

        }
        public List<Transaction> SortByCategory()
        {
            return _db.Transactions
                .Include(t => t.Category)
                .OrderByDescending(t => t.Category.Name)
                .ToList();
        }
        public List<Transaction> SortByDate()
        {
            return _db.Transactions
                .OrderByDescending(x => x.Date)
                .ToList();
        }
    }
}
