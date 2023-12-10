using DTO;
using FontAwesome.Sharp;
using GUI.Commands;
using GUI.Views;
using RM_Project1.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace GUI.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
        private DataViewModel? _dataViewModel;
        public DataViewModel? DataViewModel
        {
            get { return _dataViewModel; }
            set { _dataViewModel = value; OnPropertyChanged(nameof(DataViewModel)); }
        }

        private OrderDataViewModel? _orderDataViewModel;
        public OrderDataViewModel? OrderDataViewModel
        {
            get { return _orderDataViewModel; }
            set { _orderDataViewModel = value; OnPropertyChanged(nameof(OrderDataViewModel)); }
        }



        private string _titleName = string.Empty;
        public string TitleName
        {
            get { return _titleName; }
            set { _titleName = value; OnPropertyChanged(nameof(TitleName)); }
        }

        private IconChar? _titleICon = null;
        public IconChar? TitleIcon
        {
            get { return _titleICon; }
            set { _titleICon = value; OnPropertyChanged(nameof(TitleIcon)); }
        }



        private UserControl? _childView;
        public UserControl? ChildView
        {
            get { return _childView; }
            set { _childView = value; OnPropertyChanged(nameof(ChildView)); }
        }



        private TableAreaView? _tableAreaView;
        public TableAreaView? TableAreaView
        {
            get { return _tableAreaView; }
            set { _tableAreaView = value; OnPropertyChanged(nameof(TableAreaView)); }
        }

        private UserControl? _tableAreaChildView;
        public UserControl? TableAreaChildView
        {
            get { return _tableAreaChildView; }
            set { _tableAreaChildView = value; OnPropertyChanged(nameof(TableAreaChildView)); }
        }

        private TableView? _tableView;
        public TableView? TableView
        {
            get { return _tableView; }
            set { _tableView = value; OnPropertyChanged(nameof(TableView)); }
        }

        private AreaView? _areaView;
        public AreaView? AreaView
        {
            get { return _areaView; }
            set { _areaView = value; OnPropertyChanged(nameof(AreaView)); }
        }

        private AreaDTO? _selectedArea;
        public AreaDTO? SelectedArea
        {
            get { return _selectedArea; }
            set { _selectedArea = value; OnPropertyChanged(nameof(SelectedArea)); }
        }



        private ProductCategoryView? productCategoryView;
        public ProductCategoryView? ProductCategoryView
        {
            get { return productCategoryView; }
            set { productCategoryView = value; OnPropertyChanged(nameof(ProductCategoryView)); }
        }

        private UserControl? _productCategoryChildView;
        public UserControl? ProductCategoryChildView
        {
            get { return _productCategoryChildView; }
            set { _productCategoryChildView = value; OnPropertyChanged(nameof(ProductCategoryChildView)); }
        }

        private ProductView? _productView;
        public ProductView? ProductView
        {
            get { return _productView; }
            set { _productView = value; OnPropertyChanged(nameof(ProductView)); }
        }

        private CategoryView? _categoryView;
        public CategoryView? CategoryView
        {
            get { return _categoryView; }
            set { _categoryView = value; OnPropertyChanged(nameof(CategoryView)); }
        }

        private CategoryDTO? _selectedCategory;
        public CategoryDTO? SelectedCategory
        {
            get { return _selectedCategory; }
            set { _selectedCategory = value; OnPropertyChanged(nameof(SelectedCategory)); }
        }



        private EmployeeView? _employeeView;
        public EmployeeView? EmployeeView
        {
            get { return _employeeView; }
            set { _employeeView = value; OnPropertyChanged(nameof(EmployeeView)); }
        }
        


        private UserControl? _orderTableView;
        public UserControl? OrderTableView
        {
            get => _orderTableView;
            set { _orderTableView = value; OnPropertyChanged(nameof(OrderTableView)); }
        }

        private ValidTableView? _validTableView;
        public ValidTableView? ValidTableView
        {
            get => _validTableView;
            set { _validTableView = value; OnPropertyChanged(nameof(ValidTableView)); }
        }

        private OrderView? _orderView;
        public OrderView? OrderView
        {
            get => _orderView;
            set { _orderView = value; OnPropertyChanged(nameof(OrderView)); }
        }




        // Commands
        public ICommand? LogOutCommand { get; set; }


        public ICommand? ShowTableAreaViewCommand { get; set; }
        public ICommand? ShowTableViewCommand { get; set; }
        public ICommand? ShowAreaViewCommand { get; set; }
        public ICommand? ShowAreaInfoFormViewCommand { get; set; }


        public ICommand? ShowProductCategoryViewCommand { get; set; }
        public ICommand? ShowProductViewCommand { get; set; }
        public ICommand? ShowCategoryViewCommand { get; set; }
        public ICommand? ShowCategoryInfoFormViewCommand { get; set; }
        public ICommand? ShowProductInfoFormViewCommand { get; set; }


        public ICommand? ShowEmployeeViewCommand { get; set; }
        public ICommand? ShowEmployeeInfoFormViewCommand { get; set; }


        //public ICommand? ShowOrderTableViewCommand { get; set; }
        public ICommand? ShowValidTableViewCommand { get; set; }
        public ICommand? ShowOrderViewCommand { get; set; }


        // Constructors
        public MainViewModel()
        {
            if (UserSection.Instance == null) return;
            if (UserSection.Instance.RoleName == null) return;
            var svc = ServiceConfiguration.GetApplicationServiceProvider(UserSection.Instance.RoleName);
            if (svc == null) return;
            DataViewModel = new DataViewModel(svc);
            OrderDataViewModel = new OrderDataViewModel(svc);
            DataViewModel.CurrentUser = UserSection.Instance;


            // Sets Commands
            LogOutCommand = new RelayCommand(ExecuteLogOutCommand);

            ShowTableAreaViewCommand = new RelayCommand(ExecuteShowTableAreaViewCommand);
            ShowTableViewCommand = new RelayCommand(ExecuteShowTableViewCommand);
            ShowAreaViewCommand = new RelayCommand(ExecuteShowAreaViewCommand);
            ShowAreaInfoFormViewCommand = new RelayCommand(ExecuteShowAreaInfoFormViewCommand);

            ShowProductCategoryViewCommand = new RelayCommand(ExecuteShowProductCategoryViewCommand);
            ShowProductViewCommand = new RelayCommand(ExecuteShowProductViewCommand);
            ShowCategoryViewCommand = new RelayCommand(ExecuteShowCategoryViewCommand);
            ShowCategoryInfoFormViewCommand = new RelayCommand(ExecuteShowCategoryInfoFormViewCommand);
            ShowProductInfoFormViewCommand = new RelayCommand(ExecuteShowProductInfoFormViewCommand);

            ShowEmployeeViewCommand = new RelayCommand(ExecuteShowEmployeeViewCommand);
            ShowEmployeeInfoFormViewCommand = new RelayCommand(ExecuteShowEmployeeInfoFormViewCommand);

            //ShowOrderTableViewCommand = new RelayCommand(ExecuteShowOrderTableViewCommand);
            ShowValidTableViewCommand = new RelayCommand(ExecuteShowValidTableViewCommand);
            ShowOrderViewCommand = new RelayCommand(ExecuteShowOrderViewCommand);

            // Start Commands
            ShowValidTableViewCommand?.Execute(null);
        }


        private void ExecuteShowTableAreaViewCommand(object? obj)
        {
            if (TableAreaView == null)
            {
                TableAreaView = new TableAreaView();
                ShowTableViewCommand?.Execute(null);
            }
            ChildView = TableAreaView;
            TitleName = TableAreaView.TitleName;
            TitleIcon = TableAreaView.TitleIcon;
        }
        private void ExecuteShowTableViewCommand(object? obj)
        {
            if (DataViewModel == null) return;
            if (TableView == null) TableView = new TableView();
            TableAreaChildView = TableView;
        }
        private void ExecuteShowAreaViewCommand(object? obj)
        {
            if (DataViewModel == null) return;
            if (AreaView == null) AreaView = new AreaView();
            TableAreaChildView = AreaView;
        }
        private void ExecuteShowAreaInfoFormViewCommand(object? obj)
        {
            ActionView areaActionView = new ActionView(new AreaInfoFormView());
            var dataContext = new AreaActionViewModel(DataViewModel, ShowAreaViewCommand, obj);
            dataContext.SetBtnMode("clear,setdefault,update");
            areaActionView.DataContext = dataContext;
            TableAreaChildView = areaActionView;
        }


        private void ExecuteShowProductCategoryViewCommand(object? obj)
        {
            if (ProductCategoryView == null)
            {
                ProductCategoryView = new ProductCategoryView();
                ShowProductViewCommand?.Execute(null);
            }
            ChildView = ProductCategoryView;
            TitleName = ProductCategoryView.TitleName;
            TitleIcon = ProductCategoryView.TitleIcon;
        }
        private void ExecuteShowProductViewCommand(object? obj)
        {
            if (DataViewModel == null) return;
            if (ProductView == null) ProductView = new ProductView();
            ProductCategoryChildView = ProductView;
        }
        private void ExecuteShowCategoryViewCommand(object? obj)
        {
            if (DataViewModel == null) return;
            if (CategoryView == null) CategoryView = new CategoryView();
            ProductCategoryChildView = CategoryView;
        }
        private void ExecuteShowCategoryInfoFormViewCommand(object? obj)
        {
            ActionView areaActionView = new ActionView(new CategoryInfoFormView());
            var dataContext = new CategogryActionViewModel(DataViewModel, ShowCategoryViewCommand, obj);
            if (obj == null)
                dataContext.SetBtnMode("clear, add");
            else
                dataContext.SetBtnMode("clear, setdefault, update, delete");
            areaActionView.DataContext = dataContext;
            ProductCategoryChildView = areaActionView;
        }
        private void ExecuteShowProductInfoFormViewCommand(object? obj)
        {
            ActionView areaActionView = new ActionView(new ProductInfoFormView());
            var dataContext = new ProductActionViewModel(DataViewModel, ShowProductViewCommand, obj);
            if (obj == null)
                dataContext.SetBtnMode("clear, add");
            else
                dataContext.SetBtnMode("clear, setdefault, update, delete");
            areaActionView.DataContext = dataContext;
            ProductCategoryChildView = areaActionView;
        }


        private void ExecuteShowEmployeeViewCommand(object? obj)
        {
            if (EmployeeView == null)
            {
                EmployeeView = new EmployeeView();
            }
            ChildView = EmployeeView;
            TitleName = EmployeeView.TitleName;
            TitleIcon = EmployeeView.TitleIcon;
        }
        private void ExecuteShowEmployeeInfoFormViewCommand(object? obj)
        {
            ActionView areaActionView = new ActionView(new EmployeeInfoFormView());
            var dataContext = new EmployeeActionViewModel(DataViewModel, ShowEmployeeViewCommand, obj);
            if (obj == null)
                dataContext.SetBtnMode("clear, add");
            else
                dataContext.SetBtnMode("clear, setdefault, update, delete");
            EmployeeView = null;
            areaActionView.DataContext = dataContext;
            ChildView = areaActionView;
        }


        //private void ExecuteShowOrderTableViewCommand(object? obj)
        //{
        //    if (OrderTableView == null)
        //    {
        //        OrderTableView = new UserControl();
        //        ShowValidTableViewCommand?.Execute(null);
        //    }
        //    ChildView = OrderTableView;
        //    TitleName = "Order";
        //    TitleIcon = IconChar.ShoppingCart;
        //}

        private void ExecuteShowValidTableViewCommand(object? obj)
        {
            if (ValidTableView == null)
                ValidTableView = new ValidTableView();
            ChildView = ValidTableView;
            TitleName = "Order";
            TitleIcon = IconChar.ShoppingCart;
        }

        private void ExecuteShowOrderViewCommand(object? obj)
        {
            if (OrderView == null)
                OrderView = new OrderView();
            ChildView = OrderView;
            TitleName = "Order";
            TitleIcon = IconChar.ShoppingCart;
        }






        private void ExecuteLogOutCommand(object? obj)
        {
            WindowManager.ShowLoginWindow();
            WindowManager.CloseWindow(this);
            UserSection.ClearUser();
        }
    }
}
