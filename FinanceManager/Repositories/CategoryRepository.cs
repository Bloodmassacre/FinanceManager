using FinanceManager.Models;
using FinanceManager.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceManager.Repositories
{
    public class CategoryRepository : BaseRepository<Category>
    {
        private readonly Database _db = new Database();
        public CategoryRepository()
        {

        }
        //public void GetTransactions()
        //{

        //}
        public Category AddCategory(string name, string icon, string color)
        {
            var category = new Category()
            {
                Name = name,
                Icon = icon,
                Color = color
            };
            //_db.Category.Add(category);
            _db.SaveChanges();
            return category;
        }
    }
}
