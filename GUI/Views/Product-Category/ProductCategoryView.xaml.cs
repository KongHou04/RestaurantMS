using FontAwesome.Sharp;
using System.Windows.Controls;

namespace GUI.Views
{
    /// <summary>
    /// Interaction logic for ProductCategoryView.xaml
    /// </summary>
    public partial class ProductCategoryView : UserControl
    {
        public static string TitleName = "Product - Category";
        public static IconChar TitleIcon = IconChar.Burger;
        public ProductCategoryView()
        {
            InitializeComponent();
            productTab.IsChecked = true;
        }
    }
}
