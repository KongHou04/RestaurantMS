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
using System.Windows.Shapes;

namespace RM_Project1.Views
{
    /// <summary>
    /// Interaction logic for AutoWindow.xaml
    /// </summary>
    public partial class AutoWindow : Window
    {
        public AutoWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string str = string.Empty;
            for (int i = 0; i < 50; i++)
            {
                if (i < 25)
                    str += "(" + (i+1) + ", false, false, " + AreaID.Text + ")," + "\n";
                else
                    str += "(" + (i+1) + ", false, false, " + AreaID.Text + ")," + "\n";
            }
            text.Text = str;

        }
    }
}
