using Avalonia.Data.Converters;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FinanceManager.Data;

namespace FinanceManager.Converters
{
    public class ConvertToIcon : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is int categoryId && categoryId > 0)
            {
                using (var db = new Database())
                {
                    var category = db.Categories.Find(categoryId);
                    return category?.Icon ?? "⛔";
                }
            }
            return "⛔";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
