using System;
using System.Collections.Generic;
using System.Linq;
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

namespace GUI.Views
{
    /// <summary>
    /// Interaction logic for ProductInfoFormView.xaml
    /// </summary>
    public partial class ProductInfoFormView : UserControl
    {
        public ProductInfoFormView()
        {
            InitializeComponent();

        }
        private void TextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            var o = sender as TextBox;
            if (o != null)
            {
                if (o.Text.Length == 0)
                    o.Text = 0.ToString();
            }
            // Kiểm tra xem chuỗi mới nhập có phải là số hay không
            if (!int.TryParse(e.Text, out _))
            {
                // Nếu không phải là số, ngăn chặn sự kiện PreviewTextInput
                e.Handled = true;
            }
        }
    }
}
