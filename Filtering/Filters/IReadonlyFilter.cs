namespace Filtering.Filters
{
    public interface IReadonlyFilter<TFilteredItem>
    {
        bool IsActivated { get; set; }

        bool ApplyTo(TFilteredItem item);
    }
}
