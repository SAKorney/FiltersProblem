using System;

namespace Filtering.Filters
{
    [Obsolete("Использовать FilterByValue<Item, string, string>")]
    public class FilterByText<T> : AbstractFilter<T, string>
    {
        protected string _filterValue;

        public FilterByText(
            string text = default,
            Func<T, string> extractingValueMethod = null) : base (extractingValueMethod)
        {
            if (text == default)
                throw new ArgumentException("Пустое значение фильтра");

            _filterValue = text;
        }

        protected override bool _checkCondition(string checkedValue)
        {
            return checkedValue.Contains(_filterValue);
        }
    }
}
