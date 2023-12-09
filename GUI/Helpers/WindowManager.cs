using GUI.ViewModels;
using GUI.Views;
using System.Linq;
using System.Windows;

namespace RM_Project1.Helpers
{
    public class WindowManager
    {

        public static void ShowLoginWindow()
        {
            LoginWindow loginWindow = new LoginWindow();
            loginWindow.Show();
        }

        public static void ShowMainWindow()
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
        }

        public static void CloseWindow(object viewModel)
        {
            var windows = Application.Current.Windows.Cast<Window>().ToList();
            foreach (var window in windows)
            {
                if (window.DataContext == viewModel)
                {
                    window.Close();
                    break;
                }
            }
        }
    }
}
