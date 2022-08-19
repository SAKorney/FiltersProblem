using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

using FunWithFilter.Data;
using FunWithFilter.Filter;

namespace FunWithFilter
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Filter<Item> _filter;

        public MainWindow()
        {
            InitializeComponent();

            Table.ItemsSource = new Repository().Items;

            var cv = CollectionViewSource.GetDefaultView(Table.ItemsSource);
            cv.GroupDescriptions.Add(new PropertyGroupDescription("Count"));

            _filter = new Filter<Item>();

            // Значение для фильтров всегда актуальные за счёт замыкания
            var cond0 = new Condition<Item>(x => x.Text.Contains(filterValue.Text));
            var cond1 = new Condition<Item>(x => x.Count >= int.Parse(Count.Text));
            _filter.AddCondition("TextConstraint", cond0);
            _filter.AddCondition("CountConstraint", cond1);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var cv = CollectionViewSource.GetDefaultView(Table.ItemsSource);

            cv.Filter = (obj) =>
            {
                var item = obj as Item;

                return _filter.ApplyTo(item);
            };
        }
    }
}
