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
        public List<Transaction> SortByCategory(int userId)
        {
            return _db.Transactions
                .Where(x => x.UserId == userId)
                .Include(t => t.Category)
                .OrderByDescending(t => t.Category.Name)
                .ToList();
        }
        public List<Transaction> SortByDate(int userId)
        {
            return _db.Transactions
                .Where(x => x.UserId == userId)
                .OrderByDescending(x => x.Date)
                .ToList();
        }
    }
}