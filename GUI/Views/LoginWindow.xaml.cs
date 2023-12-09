using GUI.ViewModels;
using System;
using System.Windows;

namespace GUI.Views
{
    /// <summary>
    /// Interaction logic for LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        // Demo UI
        private WindowControlView _windowControlView;

        public LoginWindow()
        {
            InitializeComponent();
            DataContext = new LoginViewModel();

            _windowControlView = new WindowControlView(windowControl_MinimizeClicked, windowControl_CloseClicked, windowControl_WindowMoveHolded);
            windowControlRegion.Content = _windowControlView.Content;
        }

        private void windowControl_MinimizeClicked(object? sender, EventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void windowControl_CloseClicked(object? sender, EventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void windowControl_WindowMoveHolded(object? sender, EventArgs e)
        {
            DragMove();
        }
    }
}
