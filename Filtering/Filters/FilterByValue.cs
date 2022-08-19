using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Filtering.Filters
{
    /// <summary>
    /// Универсальный фильтр по свойству (в том числе, составному)
    /// </summary>
    /// <typeparam name="TItem">Тип фильтруемого элемента</typeparam>
    /// <typeparam name="TFilterValue">Тип фильтруемого значения</typeparam>
    /// <typeparam name="TProperty">Тип фильтруемого поля элемента</typeparam>
    public class FilterByValue<TItem, TFilterValue, TProperty> : AbstractFilter<TItem, TProperty>
    {
        private TFilterValue _filterValue;
        private Func<TProperty, TFilterValue, bool> _predicate;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="filterValue">Значение фильтра</param>
        /// <param name="extractingPropertyMethod">Метод извлечения свойства для фильтрации</param>
        /// <param name="predicate">Метод проверки свойства на соответствие условию фильтра</param>
        public FilterByValue(
            TFilterValue filterValue,
            Func<TItem, TProperty> extractingPropertyMethod,
            Func<TProperty, TFilterValue, bool> predicate)
            : base(extractingPropertyMethod)
        {
            _filterValue = filterValue;
            _predicate = predicate;
        }

        protected override bool _checkCondition(TProperty checkedValue)
           => _predicate(checkedValue, _filterValue);
    }
}