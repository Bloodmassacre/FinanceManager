using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceManager.Repositories
{
    public class BaseRepository<T> where T : class
    {
        private List<T> _items = new List<T>();
        private int _nextId = 1;
        public List<T> GetAll => _items.ToList();

        public T Create(T item)
        {
            ((dynamic)item).Id = ++_nextId;
            _items.Add(item);
            return item;
        }
        public T GetById(int id)
        {
            return _items.FirstOrDefault(item => ((dynamic)item).Id == id);
        }
        public T? Delete(int id)
        {
            T item = GetById(id);
            if (item == null) return null;
            _items.Remove(item);
            return item;
        }

    }
}
