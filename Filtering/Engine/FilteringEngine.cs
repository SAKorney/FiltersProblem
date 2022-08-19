using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

using Filtering.Filters;

namespace Filtering
{
    public class FilteringEngine<TRepository, TItem> where TRepository : IRepository<TItem>
    {
        private TRepository _repository;
        private Dictionary<string, IReadonlyFilter<TItem>> _filters;

        public FilteringEngine(TRepository repository)
        {
            _repository = repository;
            _filters = new Dictionary<string, IReadonlyFilter<TItem>>();
        }

        public void AddFilter(string name, IReadonlyFilter<TItem> filter)
        {
            if (_filters.ContainsKey(name))
                throw new ArgumentException($"Фильтр {name} уже содержится в списке применяемых фильтров");

            _filters.Add(name, filter);
        }

        public void RemoveFilter(string name)
        {
            _filters.Remove(name);
        }

        public void ActivateFilter(string name)
        {
            _filters[name].IsActivated = true;
        }

        public void DeactivateFilter(string name)
        {
            _filters[name].IsActivated = false;
        }

        public IReadonlyFilter<TItem> this[string filterName]
        {
            get
            {
                if (false == _filters.TryGetValue(filterName, out IReadonlyFilter<TItem> filter))
                    throw new ArgumentOutOfRangeException($"Фильтр {filterName} не существует");

                return filter;
            }
        }

        public IEnumerable<TItem> Filter()
        {
            var res = _repository.Items;

            foreach (var condition in _filters.Values)
            {
                res = res.FilterBy(condition);
            }

            return res;
        }

        #region Future
        public IEnumerable<TItem> ParallelFilter(IEnumerable<IReadonlyFilter<TItem>> filters)
        {
            var res = _repository.Items;

            foreach (var filter in filters)
            {
                res = res.ParallelFilterBy(filter);
            }

            return res;
        }

        public Task<IEnumerable<TItem>> FilterAsync(IEnumerable<IReadonlyFilter<TItem>> filters)
        {
            return Task.Run(() =>
            {
                return Filter();
            });
        }

        public Task<IEnumerable<TItem>> ParallelFilterAsync(IEnumerable<IReadonlyFilter<TItem>> filters)
        {
            return Task.Run(() =>
            {
                return ParallelFilter(filters);
            });
        }
        #endregion
    }
}
