using BLL.Interfaces;
using BLL.Services;
using DAL.Contexts;
using DAL.Interfaces;
using DAL.Models;
using DTO;
using GUI.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
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
    /// Interaction logic for LoginView.xaml
    /// </summary>
    public partial class LoginView : UserControl
    {
        public LoginView()
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
                ((LoginViewModel)this.DataContext).Password = ((PasswordBox)sender).Password;
            }

        }

    }
}
