using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Filtering
{
    public class Item
    {
        private static int _id = 1;

        private static int GetId() => _id++;

        public readonly int Id;

        public readonly int Count;
        public readonly string Text;
        public readonly DateTime CreationDateTime;

        public Item(int count, string text, DateTime creationDate)
        {
            Id = GetId();
            Count = count;
            Text = text;
            CreationDateTime = creationDate;
        }

        public override string ToString()
            => $"Item Id_{Id}:{Environment.NewLine}Text: {Text} Count: {Count} Creation Date: {CreationDateTime}";
    }

    class Repository : IRepository<Item>
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

    public static class ItemExtenstion
    {
        public static string GetString(this Item item) => item.Text;
        public static int GetCount(this Item item) => item.Count;
        public static DateTime? GetDateTime(this Item item) => item.CreationDateTime;
    }
}
