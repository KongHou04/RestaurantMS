using BLL.Interfaces;
using DAL.Models;
using FontAwesome.Sharp;
using System.Collections.ObjectModel;
using System.Windows.Controls;

namespace GUI.Views
{
    /// <summary>
    /// Interaction logic for HistoryAnalytisView.xaml
    /// </summary>
    public partial class HistoryAnalytisView : UserControl
    {
        public const string TitleName = "History - Analytis";
        public const IconChar TitleIcon = IconChar.ClockRotateLeft;

        public ObservableCollection<Order>? OrderList;
        private readonly IOrderSVC _orderSVC;

        public HistoryAnalytisView()
        {
            InitializeComponent();
        }
        public HistoryAnalytisView(IOrderSVC orderSVC)
        {
            _orderSVC = orderSVC;
            InitializeComponent();
        }
    }
}
