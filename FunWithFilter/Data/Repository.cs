using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FunWithFilter.Data
{
    class Repository
    {
        private List<Item> _items = new List<Item>();

        public Repository()
        {
            var today = DateTime.Now;
            var tomorrow = today.AddDays(1);
            var yesterday = today.AddDays(-1);

            _items.Add(new Item(1, "имя цели обрабатываемого правила", yesterday));
            _items.Add(new Item(2, "имя первой зависимости обрабатываемого правила", yesterday));
            _items.Add(new Item(3, "список всех зависимостей обрабатываемого правила", yesterday));

            _items.Add(new Item(1, "имя цели обрабатываемого правила", today));
            _items.Add(new Item(2, "имя первой зависимости обрабатываемого правила", today));
            _items.Add(new Item(3, "список всех зависимостей обрабатываемого правила", today));

            _items.Add(new Item(1, "имя цели обрабатываемого правила", tomorrow));
            _items.Add(new Item(2, "имя первой зависимости обрабатываемого правила", tomorrow));
            _items.Add(new Item(3, "список всех зависимостей обрабатываемого правила", tomorrow));
        }

        public IEnumerable<Item> Items => _items;
    }
}
