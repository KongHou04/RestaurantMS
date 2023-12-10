using BLL.Interfaces;
using BLL.Services;
using DTO;
using GUI.Commands;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace GUI.ViewModels
{
    public class OrderDataViewModel : BaseViewModel
    {
        private readonly IOrderSVC? _orderSVC;


        #region Fields - Properties
        private ObservableCollection<IGrouping<string?, TableDTO>>? _tables;
        public ObservableCollection<IGrouping<string?, TableDTO>>? Tables
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


        #region Commands
        public ICommand? RefreshDataCommand { get; set; }

        #endregion




        public OrderDataViewModel(ServiceProvider services)
        {
            _orderSVC = services.GetService<IOrderSVC>();

            RefreshDataCommand = new RelayCommand(ExecuteRefreshDataCommand);

            RefreshDataCommand?.Execute(null);
        }

        private void ExecuteRefreshDataCommand(object? obj)
        {
            UpdateAreaFilterList();
            UpdateTablesList();
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
        private void UpdateTablesList()
        {
            if (_orderSVC == null || SelectedAreaFilter == null) return;
            ObservableCollection<TableDTO> t;
            if (SelectedAreaFilter.Name?.ToLower() == "All".ToLower())
                t = new ObservableCollection<TableDTO>(_orderSVC.GetAllValidTablesByArea(null));
            else
                t = new ObservableCollection<TableDTO>(_orderSVC.GetAllValidTablesByArea(SelectedAreaFilter));
            Tables = new ObservableCollection<IGrouping<string?, TableDTO>>(t.GroupBy(o => o.AreaName));
        }
        






    }
}
