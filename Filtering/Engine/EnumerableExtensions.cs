using System.Collections.Generic;
using System.Linq;

using Filtering.Filters;

namespace Filtering
{
    public static class Extensions
    {
        public static IEnumerable<T> FilterBy<T>(this IEnumerable<T> enumerator, IReadonlyFilter<T> condition)
        {
            return condition.IsActivated ?
                enumerator.Where(item => condition.ApplyTo(item)) :
                enumerator;
        }

        public static IEnumerable<T> ParallelFilterBy<T>(this IEnumerable<T> enumerator, IReadonlyFilter<T> filter)
        {
            return filter.IsActivated ?
                enumerator.AsParallel().Where(item => filter.ApplyTo(item)) :
                enumerator;
        }
    }
}
