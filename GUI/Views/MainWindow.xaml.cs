using GUI.ViewModels;
using FontAwesome.Sharp;
using Microsoft.VisualBasic.ApplicationServices;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using System.Windows.Shapes;

namespace GUI.Views
{
    /// <summary>
    /// Interaction logic for Main.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        // Title Change Properties
        KeyValuePair<string, IconChar>[] titles =
        {
            new KeyValuePair<string, IconChar>("Order", IconChar.ShoppingCart),
            new KeyValuePair<string, IconChar>("Product", IconChar.Burger),
            new KeyValuePair<string, IconChar>("Table", IconChar.Table),
            new KeyValuePair<string, IconChar>("History", IconChar.ClockRotateLeft),
            new KeyValuePair<string, IconChar>("Employeee", IconChar.UserAlt),
            new KeyValuePair<string, IconChar>("Settings", IconChar.Gear),
        };


        public MainWindow()
        {
            InitializeComponent();
            DataContext = new MainViewModel();

            MaxHeight = SystemParameters.MaximizedPrimaryScreenHeight;
            MaxWidth = SystemParameters.MaximizedPrimaryScreenWidth;

            // Setting properties to change Title
            // Dont touch it plz
            rdBtnOrder.Tag = titles[0];
            rdBtnFoodnDrink.Tag = titles[1];
            rdBtnTable.Tag = titles[2];
            rdBtnHistory.Tag = titles[3];
            rdBtnEmployee.Tag = titles[4];
            rdBtnSettings.Tag = titles[5];


            // Đặt nội dung của ContentControl là Window2

            //dataPanel.ItemsSource = Users;

            rdBtnOrder.IsChecked = true;
            
        }

        private void windowBorder_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ClickCount == 2)
                if (WindowState == WindowState.Normal)
                    WindowState = WindowState.Maximized;
                else
                    WindowState = WindowState.Normal;
            else
            {
                try
                {
                    DragMove();
                }
                catch { }
            }
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void btnMaximize_Click(object sender, RoutedEventArgs e)
        {
            if (WindowState == WindowState.Normal)
                WindowState = WindowState.Maximized;
            else
                WindowState = WindowState.Normal;
        }

        private void btnMinimize_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void titleBtn_Checked(object sender, RoutedEventArgs e)
        {
            var myRdBtn = sender as RadioButton;
            if (myRdBtn == null)
            {
                MessageBox.Show(":D");
                return;
            }
            try
            {
                if (myRdBtn.Tag is KeyValuePair<string, IconChar> holder)
                {
                    txtTitle.Text = holder.Key;
                    titleIcon.Icon = holder.Value;
                }
                else
                {
                    MessageBox.Show("Nah");
                }
            }
            catch { }
        }

        private void btnLogout_Click(object sender, RoutedEventArgs e)
        {
        }

        private void rdBtnTable_Checked(object sender, RoutedEventArgs e)
        {
            titleBtn_Checked(sender, e);
            //contenControl.Content = ChildView.Content;
        }

    }

}
