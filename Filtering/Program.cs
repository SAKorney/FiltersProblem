using System;

using Filtering.Filters;

namespace Filtering
{
    class Program
    {
        const string TextFilter = nameof(TextFilter);
        const string CountFilter = nameof(CountFilter);
        const string DateFilter = nameof(DateFilter);

        static void Main(string[] args)
        {
            var repository = new Repository();

            Console.WriteLine("Repository contains:");
            foreach (var item in repository.Items)
            {
                Console.WriteLine(item);
            }

            // var filter0 = new FilterByText<Item>("имя", x => x.Text);
            var filter0 = new FilterByValue<Item, string, string>("имя", x => x.Text, (v, t) => v.Contains(t));

            var filter1 = new FilterByRange<Item, int>(x => x.Count);
            filter1.AddLeftBound(1, InequalityRelation.GreaterOrEqual);
            filter1.AddRightBound(2, InequalityRelation.LessOrEqual);

            //var filter2 = new FilterByValue<Item, DateTime, DateTime>

            var engine = new FilteringEngine<Repository, Item>(repository);
            engine.AddFilter(TextFilter, filter0);
            engine.AddFilter(CountFilter, filter1);

            var res = engine.Filter();

            Console.WriteLine();
            Console.WriteLine("After filtering:");
            foreach (var item in res)
            {
                Console.WriteLine(item);
            }

            engine.RemoveFilter(TextFilter);
            res = engine.Filter();

            Console.WriteLine();
            Console.WriteLine("After remove TextFilter:");
            foreach (var item in res)
            {
                Console.WriteLine(item);
            }


            Console.WriteLine();
            Console.WriteLine("Press [Enter] to exit");
            Console.ReadKey();
        }
    }
}
