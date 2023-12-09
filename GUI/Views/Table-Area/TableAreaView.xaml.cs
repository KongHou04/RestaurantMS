using FontAwesome.Sharp;
using System.Windows;
using System.Windows.Controls;

namespace GUI.Views
{
    /// <summary>
    /// Interaction logic for TableAndRegionView.xaml
    /// </summary>
    public partial class TableAreaView : UserControl
    {
        public const string TitleName = "Table - Area";
        public const IconChar TitleIcon = IconChar.Table;
        public TableAreaView()
        {
            InitializeComponent();
            tableTab.IsChecked = true;
            tableTab_Checked(new object(), new RoutedEventArgs());
        }

        private void tableTab_Checked(object sender, RoutedEventArgs e)
        {
            tableTab.Command?.Execute(null);
        }

        private void regionTab_Checked(object sender, RoutedEventArgs e)
        {
            regionTab.Command?.Execute(null);
        }
    }
}
