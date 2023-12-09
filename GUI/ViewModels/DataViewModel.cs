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
        #endregion


        #region Fields - Properties

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

        private string _productsSearchTerm;
        public string ProductSearchTerm
        {
            get { return _productsSearchTerm; }
            set
            {
                _productsSearchTerm = value;
                OnPropertyChanged(nameof(ProductSearchTerm));
                UpdateProductsList();
            }
        }
        #endregion

        #endregion


        #region Commands
        public ICommand? RefreshDataCommand { get; set; }
        public ICommand? AddTablesCommand { get; set; }
        public ICommand? DeleteTablesCommand { get; set; }

        #endregion


        #region Constructor
        public DataViewModel(ServiceProvider services)
        {
            _areaSearchTerm = string.Empty;
            _categorySearchTerm= string.Empty;
            _productsSearchTerm= string.Empty;

            // Sets Services
            _areaSVC = services.GetService<IAreaSVC>();
            _categorySVC = services.GetService<ICategorySVC>();
            _productSVC = services.GetService<IProductSVC>();


            // Sets Commands
            RefreshDataCommand = new RelayCommand(ExecuteRefreshData);
            AddTablesCommand = new RelayCommand(ExecuteAddTablesCommand);
            DeleteTablesCommand = new RelayCommand(ExecuteDeleteTablesCommand);

            RefreshDataCommand?.Execute(null);
            SelectedAreaFilter = AreaFilter?.FirstOrDefault();

        }

        #endregion


        #region Methods
        private void ExecuteRefreshData(object? obj)
        {
            UpdateAreasList();

            UpdateAreaFilterList();
            UpdateTablesList();

            UpdateCategoriesList();

            UpdateCategoryFilterList();
            UpdateProductsList();
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
        private void UpdateProductsList()
        {
            if (_productSVC != null && SelectedCategoryFilter != null)
            {
                var cate = (SelectedCategoryFilter.Name?.ToLower() == "All".ToLower()) ? null : SelectedCategoryFilter;
                var p = new ObservableCollection<ProductDTO>(_productSVC.GetByNameAndCategory(ProductSearchTerm, cate));
                Products = new ObservableCollection<IGrouping<string?, ProductDTO>>(p.GroupBy(o => o.CategoryName));
            }


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
            MessageBox.Show(area.Name);
            MessageBox.Show(_areaSVC.AddTables(area));
            RefreshDataCommand?.Execute(null);
        }


        #endregion



    }
}
