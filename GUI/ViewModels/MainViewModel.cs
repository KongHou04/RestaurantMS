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
        private EmployeeDTO? _employee;
        public EmployeeDTO? Employee
        {
            get { return _employee; }
            set { _employee = value; OnPropertyChanged(nameof(Employee)); }
        }

        private DataViewModel? _dataViewModel;
        public DataViewModel? DataViewModel
        {
            get { return _dataViewModel; }
            set { _dataViewModel = value; OnPropertyChanged(nameof(DataViewModel)); }
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







        // Commands
        public ICommand? LogOutCommand { get; set; }

        public ICommand? ShowTableAreaViewCommand { get; set; }
        public ICommand? ShowTableViewCommand { get; set; }
        public ICommand? ShowAreaViewCommand { get; set; }

        public ICommand? ShowProductCategoryViewCommand { get; set; }
        public ICommand? ShowProductViewCommand { get; set; }
        public ICommand? ShowCategoryViewCommand { get; set; }


        // Constructors
        public MainViewModel()
        {
            Employee = UserSection.Instance;
            if (Employee == null) return;
            if (Employee.RoleName == null) return;
            var svc = ServiceConfiguration.GetApplicationServiceProvider(Employee.RoleName);
            if (svc == null) return;
            DataViewModel = new DataViewModel(svc);


            // Sets Commands
            LogOutCommand = new RelayCommand(ExecuteLogOutCommand);

            ShowTableAreaViewCommand = new RelayCommand(ExecuteShowTableAreaViewCommand);
            ShowTableViewCommand = new RelayCommand(ExecuteShowTableViewCommand);
            ShowAreaViewCommand = new RelayCommand(ExecuteShowAreaViewCommand);

            ShowProductCategoryViewCommand = new RelayCommand(ExecuteShowProductCategoryViewCommand);
            ShowProductViewCommand = new RelayCommand(ExecuteShowProductViewCommand);
            ShowCategoryViewCommand = new RelayCommand(ExecuteShowCategoryViewCommand);


            // Start Commands
            ShowProductCategoryViewCommand?.Execute(null);
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







        private void ExecuteLogOutCommand(object? obj)
        {
            WindowManager.ShowLoginWindow();
            WindowManager.CloseWindow(this);
            UserSection.ClearUser();
        }
    }
}
