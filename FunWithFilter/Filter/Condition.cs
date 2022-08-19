using System;

namespace FunWithFilter.Filter
{
    /// <summary>
    /// Класс инкапсулирующий условия фильтрации объектов
    /// </summary>
    /// <typeparam name="T"></typeparam>
    class Condition<T>
    {
        public bool IsActivated { get; set;}
        public Predicate<T> Predicate { get; }
        public LogicalConnective Connective { get; set; }

        public Condition(Predicate<T> predicate,
            LogicalConnective connective = LogicalConnective.Conjunction,
            bool isActivated = true)
        {
            Predicate = predicate;
            IsActivated = true;
            Connective = connective;
        }
    }
}
