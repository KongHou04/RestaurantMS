using System.Windows.Controls;
using System.Windows.Input;

namespace GUI.Views
{
    /// <summary>
    /// Interaction logic for AreaInfoFormView.xaml
    /// </summary>
    public partial class CategoryInfoFormView : UserControl
    {
        public CategoryInfoFormView()
        {
            InitializeComponent();
        }
        private void TextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            // Kiểm tra xem chuỗi mới nhập có phải là số hay không
            if (!int.TryParse(e.Text, out _))
            {
                // Nếu không phải là số, ngăn chặn sự kiện PreviewTextInput
                e.Handled = true;
            }
        }
    }
}
