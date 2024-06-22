using BLL.Interfaces;
using DTO;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Interop;

namespace GUI.ViewModels
{
    public class DemoOrderDataViewModel : BaseViewModel
    {
        private readonly IOrderSVC? _orderSVC;
        private ICategorySVC? _categorySVC { get; }
        private IProductSVC? _productSVC { get; }



        // Main data
        private ObservableCollection<OrderDisplayDTO> _orderDisplayDTO = new ObservableCollection<OrderDisplayDTO>();
        public ObservableCollection<OrderDisplayDTO> OrderDisplayDTO
        {
            get { return _orderDisplayDTO; }
            set { _orderDisplayDTO = value; OnPropertyChanged(nameof(OrderDisplayDTO)); }
        }


        private ObservableCollection<IGrouping<string?, OrderDisplayDTO>>? _tableOrders;
        public ObservableCollection<IGrouping<string?, OrderDisplayDTO>>? TableOrders
        {
            get { return _tableOrders; }
            set { _tableOrders = value; OnPropertyChanged(nameof(TableOrders)); }
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
                UpdateTableOrdersList();
            }
        }


        #region Products
        private ObservableCollection<ProductDTO>? _products = new ObservableCollection<ProductDTO>();
        public ObservableCollection<ProductDTO>? Products
        {
            get { return _products; }
            set { _products = value; OnPropertyChanged(nameof(Products)); }
        }

        private ObservableCollection<IGrouping<string?, ProductDTO>>? _groupedProducts;
        public ObservableCollection<IGrouping<string?, ProductDTO>>? GroupedProducts
        {
            get { return _groupedProducts; }
            set { _groupedProducts = value; OnPropertyChanged(nameof(GroupedProducts)); }
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
                UpdateGroupedProductsList();
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
                UpdateGroupedProductsList();
            }
        }


        #endregion




        public DemoOrderDataViewModel(ServiceProvider services)
        {
            _productSearchTerm = string.Empty;
            _orderSVC = services.GetService<IOrderSVC>();
            _productSVC = services.GetService<IProductSVC>();
            _categorySVC = services.GetService<ICategorySVC>();

            var tables = _orderSVC?.GetAllValidTablesByArea(null);
            if (tables == null) return;
            foreach (var item in tables)
            {
                OrderDisplayDTO.Add(new OrderDisplayDTO
                {
                    Table = item,
                    Order = null,
                    ODList = null,
                });
            }

            SetDefaultValues();
        }

        private void SetDefaultValues()
        {
            SetDefaultProductsList();
            UpdateTableOrdersList();
            UpdateAreaFilterList();
            SetDefaultCategoryFilterList();
            UpdateGroupedProductsList();
        }


        private void UpdateTableOrdersList()
        {
            if (SelectedAreaFilter == null) return;
            ObservableCollection<OrderDisplayDTO> t;
            if (SelectedAreaFilter.Name?.ToLower() == "All".ToLower())
                t = new ObservableCollection<OrderDisplayDTO>(OrderDisplayDTO);
            else
                t = new ObservableCollection<OrderDisplayDTO>(OrderDisplayDTO.Where(o => o.Table?.AreaID == SelectedAreaFilter.ID));
            TableOrders = new ObservableCollection<IGrouping<string?, OrderDisplayDTO>>(t.GroupBy(o => o.Table?.AreaName));
        }
        private void UpdateAreaFilterList()
        {
            if (_orderSVC != null)
            {
                AreaFilter = new ObservableCollection<AreaDTO>(_orderSVC.GetAllValidAreas());
                AreaFilter.Insert(0, new AreaDTO { Name = "All" });
            }
            if (SelectedAreaFilter != null)
                SelectedAreaFilter = AreaFilter?.FirstOrDefault(o => o.ID == SelectedAreaFilter.ID);
            else
                SelectedAreaFilter = AreaFilter?.FirstOrDefault(o => o.Name?.ToLower() == "All".ToLower());
        }


        private void SetDefaultCategoryFilterList()
        {
            if (_categorySVC != null)
            {
                CategoryFilter = new ObservableCollection<CategoryDTO>(_categorySVC.GetAll().Where(r => r.Status == true));
                CategoryFilter.Insert(0, new CategoryDTO { Name = "All" });
            }
            if (SelectedCategoryFilter != null)
                SelectedCategoryFilter = CategoryFilter?.FirstOrDefault(o => o.ID == SelectedCategoryFilter.ID);
            else
                SelectedCategoryFilter = CategoryFilter?.FirstOrDefault(o => o.Name?.ToLower() == "All".ToLower());
        }
        private void SetDefaultProductsList()
        {
            if (_productSVC != null)
            {
                Products = new ObservableCollection<ProductDTO>(_productSVC.GetAllValid());
            }
        }

        private void UpdateGroupedProductsList()
        {
            List<ProductDTO>? p;
            if (SelectedCategoryFilter?.Name?.ToLower() == "All".ToLower())
                p = Products?.Where(p => p.Name != null && p.Name.ToLower().Contains(ProductSearchTerm)).ToList();
            else
                p = Products?.Where(p => p.Name != null && p.Name.ToLower().Contains(ProductSearchTerm) && p.CategoryID == SelectedCategoryFilter?.ID).ToList();
            if (p != null)
                GroupedProducts = new ObservableCollection<IGrouping<string?, ProductDTO>>(p.GroupBy(o => o.CategoryName));
        }
        public string? PayOrder(object? obj)
        {
            if (_orderSVC == null) return null;
            var ord = obj as OrderDisplayDTO;
            return _orderSVC.PayOrder(ord);
        }

    }
}
