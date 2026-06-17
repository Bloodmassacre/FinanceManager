using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FinanceManager.Models;
using FinanceManager.Data;

namespace FinanceManager.Repositories
{
    public class BudgetRepository : BaseRepository<Budget>
    {
        private readonly Database _db = new Database();
        public BudgetRepository()
        {

        }
    }
}
