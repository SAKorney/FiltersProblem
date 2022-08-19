using System;

namespace FunWithFilter.OldConception
{
    /// <summary>
    /// Фильтр единичного элемента. По принципу, условие передаётся в метод определяющий тип логической
    /// связи с предыдущими условиями фильтрации
    /// </summary>
    /// <typeparam name="T">Тип фильтруемого объекта</typeparam>
    class Filter<T>
    {
        public bool Result { get; private set; } = true;
        private T _item;

        public Filter(T item)
        {
            _item = item;
        }

        public Filter<T> ApllyConjenctively(Predicate<T> predicate)
        {
            Result = Result && predicate(_item);
            return this;
        }

        public Filter<T> ApplyDisjunctively(Predicate<T> predicate)
        {
            Result = Result || predicate(_item);
            return this;
        }
    }
}
