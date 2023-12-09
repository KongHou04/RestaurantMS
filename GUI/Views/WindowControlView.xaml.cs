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
    /// Interaction logic for WindowControlView.xaml
    /// </summary>
    public partial class WindowControlView : UserControl
    {
        public event RoutedEventHandler? MinimizeClicked;
        public event RoutedEventHandler? MaximizeClicked;
        public event RoutedEventHandler? CloseCliked;
        public event MouseButtonEventHandler? WindowMoveHolded;
        public WindowControlView(RoutedEventHandler minimizeClicked, RoutedEventHandler maximizeClicked, RoutedEventHandler closeClicked, MouseButtonEventHandler windowMoveHolded)
        {
            InitializeComponent();
            maximizeBtn.Visibility = Visibility.Visible;
            SetEvents(minimizeClicked, maximizeClicked, closeClicked, windowMoveHolded);
        }
        public WindowControlView(RoutedEventHandler minimizeClicked, RoutedEventHandler closeClicked, MouseButtonEventHandler windowMoveHolded)
        {
            InitializeComponent();
            maximizeBtn.Visibility = Visibility.Collapsed;
            SetEvents(minimizeClicked, closeClicked, windowMoveHolded);
        }
        public void SetEvents(RoutedEventHandler minimizeClicked, RoutedEventHandler maximizeClicked, RoutedEventHandler closeClicked, MouseButtonEventHandler windowMoveHolded)
        {
            MinimizeClicked += minimizeClicked;
            MaximizeClicked += maximizeClicked;
            CloseCliked += closeClicked;
            WindowMoveHolded += windowMoveHolded;

            minimizeBtn.Click += MinimizeClicked;
            maximizeBtn.Click += MaximizeClicked;
            closeBtn.Click += CloseCliked;
            controlRegion.MouseDown += WindowMoveHolded; 
        }
        public void SetEvents(RoutedEventHandler minimizeClicked, RoutedEventHandler closeClicked, MouseButtonEventHandler windowMoveHolded)
        {
            MinimizeClicked += minimizeClicked;
            CloseCliked += closeClicked;
            WindowMoveHolded += windowMoveHolded;

            minimizeBtn.Click += MinimizeClicked;
            closeBtn.Click += CloseCliked;
            controlRegion.MouseDown += WindowMoveHolded;
        }
    }
}
