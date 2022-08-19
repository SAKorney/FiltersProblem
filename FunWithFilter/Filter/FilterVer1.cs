using System.Collections.Generic;
using System.Linq;

namespace FunWithFilter.Filter
{
    /// <summary>
    /// Класс фильтра, инкапсулирующего условия фильтрации
    /// </summary>
    /// <typeparam name="T">Тип фильтруемых объектов</typeparam>
    class Filter<T>
    {
        private Dictionary<string, Condition<T>> _conditions = new Dictionary<string, Condition<T>>();

        public void AddCondition(string condName, Condition<T> condition)
            => _conditions.Add(condName, condition);

        public void RemoveCondition(string condName)
            => _conditions.Remove(condName);

        public void ActivateCondition(string condName)
            => _conditions[condName].IsActivated = true;

        public void DeactivateCondition(string condName)
            => _conditions[condName].IsActivated = false;

        public void SetLogicalConnectiveForCondition(string condName, LogicalConnective connective)
            => _conditions[condName].Connective = connective;

        /// <summary>
        /// Проверка соответствия объекта всем условиям фильтрации
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public bool ApplyTo(T item)
        {
            bool res = true;

            foreach (var cond in _conditions.Values.Where(x => x.IsActivated))
            {
                switch (cond.Connective)
                {
                    case LogicalConnective.Conjunction:
                        res = res && cond.Predicate(item);
                        break;
                    case LogicalConnective.Disjunction:
                        res = res || cond.Predicate(item);
                        break;
                }
            }

            return res;
        }
    }
}
