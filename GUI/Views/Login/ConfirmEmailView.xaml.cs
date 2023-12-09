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
    /// Interaction logic for ConfirmEmailView.xaml
    /// </summary>
    public partial class ConfirmEmailView : UserControl
    {
        public ConfirmEmailView()
        {
            InitializeComponent();
        }
        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            var txtBox = sender as TextBox;
            var txtBlock = txtBox?.Tag as TextBlock;
            if (txtBlock != null)
                if (txtBox?.Text.Length == 0 || txtBox?.Text == null)
                    txtBlock.Visibility = Visibility.Visible;
                else
                    txtBlock.Visibility = Visibility.Collapsed;
        }
    }
}
