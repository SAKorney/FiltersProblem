using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FunWithFilter.Data
{
    /// <summary>
    /// Класс для тестирования
    /// </summary>
    public class Item
    {
        private static int _id = 1;

        private static int GetId() => _id++;

        public int Id { get; }

        public int Count { get; }
        public string Text { get; }
        public DateTime CreationDateTime { get; }

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
}
