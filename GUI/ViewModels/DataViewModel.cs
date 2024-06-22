using BLL.Interfaces;
using BLL.Services;
using DAL.Contexts;
using DAL.Interfaces;
using DAL.Repositories;
using DTO;
using GUI.Commands;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Security.Policy;
using System.Windows;
using System.Windows.Input;

namespace GUI.ViewModels
{
    public class DataViewModel : BaseViewModel
    {
        #region Services
        private IAreaSVC? _areaSVC { get; }
        private ICategorySVC? _categorySVC { get; }
        private IProductSVC? _productSVC { get; }
        private IEmployeeSVC? _employeeSVC { get; }
        private ISelfUserSVC? _selfUserSVC { get; }
        private IHistorySVC? _historySVC { get; }
        #endregion


        #region Fields - Properties

        private EmployeeDTO? _currentUser;
        public EmployeeDTO? CurrentUser
        {
            get { return _currentUser; }
            set { _currentUser = value; OnPropertyChanged(nameof(CurrentUser)); }
        }

        #region Areas
        private ObservableCollection<AreaDTO>? _areas;
        public ObservableCollection<AreaDTO>? Areas
        {
            get { return _areas; }
            set { _areas = value; OnPropertyChanged(nameof(Areas)); }
        }

        private string _areaSearchTerm;
        public string AreaSearchTerm
        {
            get { return _areaSearchTerm; }
            set 
            { 
                _areaSearchTerm = value; 
                OnPropertyChanged(nameof(AreaSearchTerm));
                UpdateAreasList();
            }
        }
        #endregion

        #region Tables
        private ObservableCollection<TableDTO>? _tables;
        public ObservableCollection<TableDTO>? Tables
        {
            get { return _tables; }
            set { _tables = value; OnPropertyChanged(nameof(Tables)); }
        }

        private ObservableCollection<AreaDTO>? _areaFilter;
        public ObservableCollection<AreaDTO>? AreaFilter
        {
            get { return _areaFilter; }
            set { _areaFilter = value; OnPropertyChanged(nameof(AreaFilter)); }
        }

        private AreaDTO? _selectedAreaFilter;
        public AreaDTO? SelectedAreaFilter
        {
            get { return _selectedAreaFilter; }
            set
            {
                _selectedAreaFilter = value;
                OnPropertyChanged(nameof(SelectedAreaFilter));
                UpdateTablesList();
            }
        }
        #endregion

        #region Categories
        private ObservableCollection<CategoryDTO>? _categories;
        public ObservableCollection<CategoryDTO>? Categories
        {
            get { return _categories; }
            set { _categories = value; OnPropertyChanged(nameof(Categories)); }
        }

        private string _categorySearchTerm;
        public string CategorySearchTerm
        {
            get { return _categorySearchTerm; }
            set
            {
                _categorySearchTerm = value;
                OnPropertyChanged(nameof(CategorySearchTerm));
                UpdateCategoriesList();
            }
        }

        #endregion

        #region Products
        private ObservableCollection<IGrouping<string?, ProductDTO>>? _products;
        public ObservableCollection<IGrouping<string?, ProductDTO>>? Products
        {
            get { return _products; }
            set { _products = value; OnPropertyChanged(nameof(Products)); }
        }

        private ObservableCollection<CategoryDTO>? _categoryFilter;
        public ObservableCollection<CategoryDTO>? CategoryFilter
        {
            get { return _categoryFilter; }
            set { _categoryFilter = value; OnPropertyChanged(nameof(CategoryFilter)); }
        }

        private CategoryDTO? _selectedCategoryFilter;
        public CategoryDTO? SelectedCategoryFilter
        {
            get { return _selectedCategoryFilter; }
            set
            {
                _selectedCategoryFilter = value;
                OnPropertyChanged(nameof(SelectedCategoryFilter));
                UpdateProductsList();
            }
        }

        private string _productSearchTerm;
        public string ProductSearchTerm
        {
            get { return _productSearchTerm; }
            set
            {
                _productSearchTerm = value;
                OnPropertyChanged(nameof(ProductSearchTerm));
                UpdateProductsList();
            }
        }

        private ObservableCollection<CategoryDTO>? _categoryFilterToEdit;
        public ObservableCollection<CategoryDTO>? CategoryFilterToEdit
        {
            get { return _categoryFilterToEdit; }
            set { _categoryFilterToEdit = value; OnPropertyChanged(nameof(CategoryFilterToEdit)); }
        }
        #endregion

        #region Employees
        private ObservableCollection<EmployeeDTO>? _employees;
        public ObservableCollection<EmployeeDTO>? Employees
        {
            get { return _employees; }
            set { _employees = value; OnPropertyChanged(nameof(Employees)); }
        }

        private string _employeeSearchTerm;
        public string EmployeeSearchTerm
        {
            get { return _employeeSearchTerm; }
            set
            {
                _employeeSearchTerm = value;
                OnPropertyChanged(nameof(EmployeeSearchTerm));
                UpdateEmployeesList();
            }
        }

        private ObservableCollection<RoleDTO>? _roleFilterToEdit;
        public ObservableCollection<RoleDTO>? RoleFilterToEdit
        {
            get { return _roleFilterToEdit; }
            set { _roleFilterToEdit = value; OnPropertyChanged(nameof(RoleFilterToEdit)); }
        }

        #endregion

        #region History
        private ObservableCollection<BillDTO>? _bills;
        public ObservableCollection<BillDTO>? Bills
        {
            get { return _bills; }
            set { _bills = value; OnPropertyChanged(nameof(Bills)); }
        }

        private DateTime _selectedDate;
        public DateTime SelectedDate
        {
            get { return _selectedDate; }
            set 
            { 
                _selectedDate = value; OnPropertyChanged(nameof(SelectedDate));
                UpdateHistoryList();
            }
        }
        #endregion

        #endregion


        #region Commands
        public ICommand? RefreshDataCommand { get; set; }

        public ICommand? AddTablesCommand { get; set; }
        public ICommand? DeleteTablesCommand { get; set; }
        public ICommand? UpdateAreaCommand { get; set; }
        public ICommand? UpdateAreaStatusCommand { get; set; }

        public ICommand? AddCategoryCommand { get; set; }
        public ICommand? UpdateCategoryCommand { get; set; }
        public ICommand? UpdateCategoryStatusCommand { get; set; }

        internal ICommand? AddProductCommand { get; private set; }
        public ICommand? UpdateProductCommand { get; set; }
        
        internal ICommand? AddEmployeeCommand { get; private set; }
        public ICommand? UpdateEmployeeCommand { get; set; }
        #endregion


        #region Constructor
        public DataViewModel(ServiceProvider services)
        {
            _selectedDate = DateTime.Now;
            _areaSearchTerm = string.Empty;
            _categorySearchTerm= string.Empty;
            _productSearchTerm= string.Empty;
            _employeeSearchTerm= string.Empty;

            // Sets Services
            _areaSVC = services.GetService<IAreaSVC>();
            _categorySVC = services.GetService<ICategorySVC>();
            _productSVC = services.GetService<IProductSVC>();
            _employeeSVC = services.GetService<IEmployeeSVC>();
            _selfUserSVC = services.GetService<ISelfUserSVC>();
            _historySVC = services.GetService<IHistorySVC>();


            // Sets Commands
            RefreshDataCommand = new RelayCommand(ExecuteRefreshData);

            AddTablesCommand = new RelayCommand(ExecuteAddTablesCommand);
            DeleteTablesCommand = new RelayCommand(ExecuteDeleteTablesCommand);
            UpdateAreaCommand = new RelayCommand(ExecuteUpdateAreaCommand);
            UpdateAreaStatusCommand = new RelayCommand(ExecuteUpdateAreaStatusCommand);

            AddCategoryCommand = new RelayCommand(ExecuteAddCategoryCommand);
            UpdateCategoryCommand = new RelayCommand(ExecuteUpdateCategoryCommand);
            UpdateCategoryStatusCommand = new RelayCommand(ExecuteUpdateCategoryStatusCommand);
            
            AddProductCommand = new RelayCommand(ExecuteAddProductCommand);
            UpdateProductCommand = new RelayCommand(ExecuteUpdateProductCommand);

            AddEmployeeCommand = new RelayCommand(ExecuteAddEmployeeCommand);
            UpdateEmployeeCommand = new RelayCommand(ExecuteUpdateEmployeeCommand);


            RefreshDataCommand?.Execute(null);
            SelectedAreaFilter = AreaFilter?.FirstOrDefault();

        }





        #endregion


        #region Methods
        private void ExecuteRefreshData(object? obj)
        {
            RefreshCurrentUser();
            UpdateAreasList();

            UpdateAreaFilterList();
            UpdateTablesList();

            UpdateCategoriesList();

            UpdateCategoryFilterList();
            UpdateCategoryFilterToEditList();
            UpdateProductsList();

            UpdateEmployeesList();
            UpdateRoleFilterToEditList();

            UpdateHistoryList();
        }


        private void RefreshCurrentUser()
        {
            if (_selfUserSVC != null && CurrentUser != null)
                _selfUserSVC.RefreshCurrentUser(CurrentUser);
        }
        private void UpdateAreasList()
        {
            if (_areaSVC != null)
                Areas = new ObservableCollection<AreaDTO>(_areaSVC.GetAllAreasByName(AreaSearchTerm));
        }
        private void UpdateAreaFilterList()
        {
            if (_areaSVC != null)
            {
                AreaFilter = new ObservableCollection<AreaDTO>(_areaSVC.GetAllAreas());
                AreaFilter.Insert(0, new AreaDTO { Name = "All" });
            }
            if (SelectedAreaFilter != null)
                SelectedAreaFilter = AreaFilter?.FirstOrDefault(o => o.ID == SelectedAreaFilter.ID);
            else
                SelectedAreaFilter = AreaFilter?.FirstOrDefault(o => o.Name?.ToLower() == "All".ToLower());
        }
        private void UpdateTablesList()
        {
            if (_areaSVC != null && SelectedAreaFilter != null)
            {
                if (SelectedAreaFilter.Name?.ToLower() == "All".ToLower())
                    Tables = new ObservableCollection<TableDTO>(_areaSVC.GetAllTables());
                else
                    Tables = new ObservableCollection<TableDTO>(_areaSVC.GetAllTablesByArea(SelectedAreaFilter));
            }
        }
        private void UpdateCategoriesList()
        {
            if (_categorySVC != null)
                Categories = new ObservableCollection<CategoryDTO>(_categorySVC.GetByName(CategorySearchTerm));
        }
        private void UpdateCategoryFilterList()
        {
            if (_categorySVC != null)
            {
                CategoryFilter = new ObservableCollection<CategoryDTO>(_categorySVC.GetAll());
                CategoryFilter.Insert(0, new CategoryDTO { Name = "All" });
            }
            if (SelectedCategoryFilter != null)
                SelectedCategoryFilter = CategoryFilter?.FirstOrDefault(o => o.ID == SelectedCategoryFilter.ID);
            else
                SelectedCategoryFilter = CategoryFilter?.FirstOrDefault(o => o.Name?.ToLower() == "All".ToLower());
        }
        private void UpdateCategoryFilterToEditList()
        {
            if (_categorySVC != null)
                CategoryFilterToEdit = new ObservableCollection<CategoryDTO>(_categorySVC.GetByName(""));
        }
        private void UpdateProductsList()
        {
            if (_productSVC != null && SelectedCategoryFilter != null)
            {
                var cate = (SelectedCategoryFilter.Name?.ToLower() == "All".ToLower()) ? null : SelectedCategoryFilter;
                var p = new ObservableCollection<ProductDTO>(_productSVC.GetByNameAndCategory(ProductSearchTerm, cate));
                Products = new ObservableCollection<IGrouping<string?, ProductDTO>>(p.GroupBy(o => o.CategoryName));
            }


        }
        private void UpdateEmployeesList()
        {
            if (_employeeSVC != null)
                Employees = new ObservableCollection<EmployeeDTO>(_employeeSVC.GetAllEmployeesByName(EmployeeSearchTerm));
        }
        private void UpdateRoleFilterToEditList()
        {
            if (_employeeSVC != null)
                RoleFilterToEdit = new ObservableCollection<RoleDTO>(_employeeSVC.GetAllRoles());
        }
        private void UpdateHistoryList()
        {
            if (_historySVC == null) return;
            Bills = new ObservableCollection<BillDTO>(_historySVC.GetAllHistoryByDate(SelectedDate));
        }



        private void ExecuteDeleteTablesCommand(object? obj)
        {
            if (_areaSVC == null) return;
            var area = obj as AreaDTO;
            if (area == null) return;
            MessageBox.Show(_areaSVC.DeleteTables(area));
            RefreshDataCommand?.Execute(null);
        }
        private void ExecuteAddTablesCommand(object? obj)
        {
            if (_areaSVC == null) return;
            var area = obj as AreaDTO;
            if (area == null) return;
            MessageBox.Show(_areaSVC.AddTables(area));
            RefreshDataCommand?.Execute(null);
        }
        private void ExecuteUpdateAreaCommand(object? obj)
        {
            if (_areaSVC == null) return;
            var area = obj as AreaDTO;
            if (area == null) return;
            string msg = _areaSVC.UpdateArea(area);
            MessageBox.Show(msg);
            RefreshDataCommand?.Execute(null);
        }
        private void ExecuteUpdateAreaStatusCommand(object? obj)
        {
            if (_areaSVC == null) return;
            var area = obj as AreaDTO;
            if (area == null) return;
            area.Status = !area.Status;
            _areaSVC.UpdateArea(area);
            RefreshDataCommand?.Execute(null);
        }

        private void ExecuteAddCategoryCommand(object? obj)
        {
            if (_categorySVC == null) return;
            var cate = obj as CategoryDTO;
            if (cate == null) return;
            string msg = _categorySVC.Add(cate);
            MessageBox.Show(msg);
            RefreshDataCommand?.Execute(null);
        }
        private void ExecuteUpdateCategoryCommand(object? obj)
        {
            if (_categorySVC == null) return;
            var cate = obj as CategoryDTO;
            if (cate == null) return;
            string msg = _categorySVC.Update(cate);
            MessageBox.Show(msg);
            RefreshDataCommand?.Execute(null);
        }
        private void ExecuteUpdateCategoryStatusCommand(object? obj)
        {
            if (_categorySVC == null) return;
            var cate = obj as CategoryDTO;
            if (cate == null) return;
            cate.Status = !cate.Status;
            string msg = _categorySVC.Update(cate);
            MessageBox.Show(msg);
            RefreshDataCommand?.Execute(null);
        }

        private void ExecuteAddProductCommand(object? obj)
        {
            if (_productSVC == null) return;
            var pro = obj as ProductDTO;
            if (pro == null) return;

            string msg = _productSVC.Add(pro);
            MessageBox.Show(msg);
            RefreshDataCommand?.Execute(null);
        }
        private void ExecuteUpdateProductCommand(object? obj)
        {
            if (_productSVC == null) return;
            var pro = obj as ProductDTO;
            if (pro == null) return;

            string msg = _productSVC.Update(pro);
            MessageBox.Show(msg);
            RefreshDataCommand?.Execute(null);
        }

        private void ExecuteAddEmployeeCommand(object? obj)
        {
            if (_employeeSVC == null) return;
            var emp = obj as EmployeeDTO;
            if (emp == null) return;
            string msg = _employeeSVC.Add(emp);
            MessageBox.Show(msg);
            RefreshDataCommand?.Execute(null);
        }
        private void ExecuteUpdateEmployeeCommand(object? obj)
        {
            if (_employeeSVC == null) return;
            var emp = obj as EmployeeDTO;
            if (emp == null) return;
            string msg = _employeeSVC.Update(emp);
            MessageBox.Show(msg);
            RefreshDataCommand?.Execute(null);
        }







        #endregion



    }
}
