using System.Collections.Generic;

namespace Filtering
{
    public interface IRepository<T>
    {
        IEnumerable<T> Items { get; }
    }
}
