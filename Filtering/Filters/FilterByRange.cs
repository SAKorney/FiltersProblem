using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Filtering.Filters
{
    public class FilterByRange<TItem, TValue> : AbstractFilter<TItem, TValue>
        where TValue : IComparable<TValue>
    {
        private FilterByInequality<TItem, TValue> _from;
        private FilterByInequality<TItem, TValue> _to;

        public FilterByRange(
            FilterByInequality<TItem, TValue> from = default,
            FilterByInequality<TItem, TValue> to = default,
            Func<TItem, TValue> extractingValueMethod = null) : base(extractingValueMethod)
        {
            if (from == default || to == default)
            {
                throw new ArgumentException("Должна быть указана как минимум одна граница диапазона");
            }

            _from = from;
            _to = to;
        }

        public FilterByRange(Func<TItem, TValue> extractingPropertyMethod = null)
            : base(extractingPropertyMethod)
        {
        }

        public void AddLeftBound(
            TValue value,
            InequalityRelation relation)
        {
            _from = new FilterByInequality<TItem, TValue>(value, relation, _extractingPropertyMethod);
            _isActivated = true;
        }

        public void AddRightBound(
            TValue value,
            InequalityRelation relation)
        {
            _to = new FilterByInequality<TItem, TValue>(value, relation, _extractingPropertyMethod);
            _isActivated = true;
        }

        protected override bool _checkCondition(TValue checkedValue)
        {
            var res = true;

            if (_from != default)
                res = res && _from.CheckValue(checkedValue);

            if (_to != default)
                res = res && _from.CheckValue(checkedValue);

            return res;
        }
    }
}
