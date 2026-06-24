using FinanceManager.Data;
using FinanceManager.Enums;
using FinanceManager.Models;
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
        public void AddDefault()
        {
            if (_db.Categories.Any())
            {
                return;
            }
            var categories = new Category[]
            {
               new Category { Name = "Зарплата", Icon = "💼", TransactionType = TransactionType.Income, IsDefault = true, Color = "Green" },
               new Category { Name = "Фриланс", Icon = "💻", TransactionType = TransactionType.Income, IsDefault = true, Color = "Green" },
               new Category { Name = "Инвестиции", Icon = "📈", TransactionType = TransactionType.Income, IsDefault = true, Color = "Green" },
               new Category { Name = "Кэшбэк", Icon = "💳", TransactionType = TransactionType.Income, IsDefault = true, Color = "Green" },
               new Category { Name = "Прочее", Icon = "📦", TransactionType = TransactionType.Income, IsDefault = true, Color = "Green" },
               new Category { Name = "Продукты", Icon = "🛒", TransactionType = TransactionType.Expense, IsDefault = true, Color = "Green" },
               new Category { Name = "Кафе и рестораны", Icon = "🍽️", TransactionType = TransactionType.Expense, IsDefault = true, Color = "Green" },
               new Category { Name = "Транспорт", Icon = "🚗", TransactionType = TransactionType.Expense, IsDefault = true, Color = "Green" },
               new Category { Name = "Жильё и ЖКХ", Icon = "🏠", TransactionType = TransactionType.Expense, IsDefault = true, Color = "Green" },
               new Category { Name = "Связь и интернет", Icon = "📱", TransactionType = TransactionType.Expense, IsDefault = true, Color = "Green" },
               new Category { Name = "Развлечения", Icon = "🎬", TransactionType = TransactionType.Expense, IsDefault = true, Color = "Green" },
               new Category { Name = "Здоровье", Icon = "💊", TransactionType = TransactionType.Expense, IsDefault = true, Color = "Green" },
               new Category { Name = "Одежда", Icon = "👔", TransactionType = TransactionType.Expense, IsDefault = true, Color = "Green" },
               new Category { Name = "Образование", Icon = "📚", TransactionType = TransactionType.Expense, IsDefault = true, Color = "Green" },
               new Category { Name = "Прочее", Icon = "📦", TransactionType = TransactionType.Expense, IsDefault = true, Color = "Green" }
            };
            _db.Categories.AddRange(categories);
            _db.SaveChanges();
        }
        public Category AddCategory(string name, string icon, string color)
        {
            var category = new Category()
            {
                Name = name,
                Icon = icon,
                Color = color
            };
            _db.Categories.Add(category);
            _db.SaveChanges();
            return category;
        }
    }
}
