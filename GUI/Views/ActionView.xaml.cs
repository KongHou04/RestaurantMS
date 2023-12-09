using System.Windows.Controls;

namespace GUI.Views
{
    /// <summary>
    /// Interaction logic for TableActionView.xaml
    /// </summary>
    public partial class ActionView : UserControl 
    {
        public ActionView(UserControl userControl)
        {
            InitializeComponent();
            contentControl.Content = userControl;
        }
        public ActionView()
        {
            InitializeComponent();
            contentControl.Content = new AreaInfoFormView();
        }
    }
}
