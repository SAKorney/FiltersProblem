using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Filtering.Filters
{
    /// <summary>
    /// Проверяемое неравенство между значениями элемента и фильтра
    /// </summary>
    public enum InequalityRelation
    {
        /// <summary>
        /// Значение должно быть меньше, чем значение фильтра
        /// </summary>
        LessThanValue,
        /// <summary>
        /// Значение должно быть больше, чем значение фильтра
        /// </summary>
        GreaterThanValue,
        /// <summary>
        /// Значение должно быть ровно значению фильтра
        /// </summary>
        Equal,
        /// <summary>
        /// Значение должно быть меньше чем значение фильтра или равно ему
        /// </summary>
        LessOrEqual,
        /// <summary>
        /// Значение должно быть больше чем значение фильтра или равно ему
        /// </summary>
        GreaterOrEqual
    }

    public class FilterByInequality<TItem, TValue> : AbstractFilter<TItem, TValue>
        where TValue : IComparable<TValue>
    {
        private Func<TValue, bool> _comparator;
        protected TValue _filterValue;

        public FilterByInequality(
            TValue filterValue = default,
            InequalityRelation relation = default,
            Func<TItem, TValue> extractingPropertyMethod = null)
            : base(extractingPropertyMethod)
        {
            if (Comparer<TValue>.Default.Compare(filterValue, default) == 0)
                throw new ArgumentException("Пустое значение фильтра");

            _filterValue = filterValue;

            switch (relation)
            {
                case InequalityRelation.LessThanValue:
                    _comparator = IsLess;
                    break;
                case InequalityRelation.GreaterThanValue:
                    _comparator = IsGreater;
                    break;
                case InequalityRelation.Equal:
                    _comparator = IsEqual;
                    break;
                case InequalityRelation.LessOrEqual:
                    _comparator = IsLessOrEqual;
                    break;
                case InequalityRelation.GreaterOrEqual:
                    _comparator = IsGreaterOrEqual;
                    break;
                default:
                    throw new NotSupportedException();
            }
        }

        protected override bool _checkCondition(TValue checkedValue)
        {
            return _comparator(checkedValue);
        }

        public bool CheckValue(TValue value)
        {
            return _comparator(value);
        }

        private bool IsLess(TValue checkedValue)
            => Comparer<TValue>.Default.Compare(checkedValue, _filterValue) < 0;

        private bool IsGreater(TValue checkedValue)
            => Comparer<TValue>.Default.Compare(checkedValue, _filterValue) > 0;

        private bool IsEqual(TValue checkedValue)
            => Comparer<TValue>.Default.Compare(checkedValue, _filterValue) == 0;

        private bool IsLessOrEqual(TValue checkedValue)
            => Comparer<TValue>.Default.Compare(checkedValue, _filterValue) <= 0;

        private bool IsGreaterOrEqual(TValue checkedValue)
            => Comparer<TValue>.Default.Compare(checkedValue, _filterValue) >= 0;
    }
}
