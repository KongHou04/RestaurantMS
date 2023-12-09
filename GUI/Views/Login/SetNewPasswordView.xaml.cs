using GUI.ViewModels;
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
    /// Interaction logic for SetNewPasswordView.xaml
    /// </summary>
    public partial class SetNewPasswordView : UserControl
    {
        public SetNewPasswordView()
        {
            InitializeComponent();
        }
        private void PasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            var txtBox = sender as PasswordBox;
            var txtBlock = txtBox?.Tag as TextBlock;
            if (txtBlock != null)
                if (txtBox?.Password.Length == 0 || txtBox?.Password == null)
                    txtBlock.Visibility = Visibility.Visible;
                else
                    txtBlock.Visibility = Visibility.Collapsed;
            if (this.DataContext != null)
            {
                if ((PasswordBox)sender == passwordTxtBox)
                    ((LoginViewModel)this.DataContext).NewPass = ((PasswordBox)sender).Password;
                else
                    ((LoginViewModel)this.DataContext).ReNewPass = ((PasswordBox)sender).Password;
            }


        }
    }
}
