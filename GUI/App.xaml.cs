using GUI.ViewModels;
using GUI.Views;
using RM_Project1.Views;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace RM_Project1
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            //new AutoWindow().Show();
            //new WindowDemo().Show();

            LoginWindow loginWindow = new LoginWindow();
            loginWindow.Show();


        }

    }
}
