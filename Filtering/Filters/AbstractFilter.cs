using System;

namespace Filtering.Filters
{
    public abstract class AbstractFilter<TItem, TProperty> : IReadonlyFilter<TItem>
    {
        protected bool _isActivated = true;
        protected Func<TItem, TProperty> _extractingPropertyMethod;
        public bool IsActivated
        {
            get => _isActivated;
            set => _isActivated = value;
        }

        public AbstractFilter(Func<TItem, TProperty> extractingPropertyMethod)
        {
            // Фильтр должен знать как извлечь из фильтруемого объекта значение для сравнения
            if (extractingPropertyMethod == null)
                throw new ArgumentNullException();

            _extractingPropertyMethod = extractingPropertyMethod;
        }

        /// <summary>
        /// Проверка условия фильтра. Всегда возвращает True, если фильтр не задан
        /// </summary>
        /// <param name="checkedItem">Проверяемый элемент</param>
        /// <returns>Результат проверки</returns>
        public bool ApplyTo(TItem checkedItem)
        {
            // Значение всегда удовлетворяет незаданному фильтру
            if (_isActivated == false)
                return true;

            var checkedValue = _extractingPropertyMethod(checkedItem);
            return _checkCondition(checkedValue);
        }

        protected abstract bool _checkCondition(TProperty checkedValue);
    }
}
