using DTO;
using GUI.Commands;
using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Input;

namespace GUI.ViewModels
{
	public class OrderActionViewModel : ActionViewModel<OrderDisplayDTO>
	{
		private DemoOrderDataViewModel? _orderDataViewModel;
		public DemoOrderDataViewModel? DemoOrderDataViewModel
		{
			get { return _orderDataViewModel; }
			set { _orderDataViewModel = value; }
		}

		private OrderDisplayDTO? _orderDisplay;
		public OrderDisplayDTO? OrderDisplay
		{
			get { return _orderDisplay; }
			set { _orderDisplay = value; OnPropertyChanged(nameof(OrderDisplay)); }
		}


		// Commands
		public ICommand? AddProductCommand { get; set; }
		public ICommand? AddQuantityCommand { get; set; }
		public ICommand? SubtractQuantityCommand { get; set; }
		public ICommand? CancelOrderCommand { get; set; }
		public ICommand? PayOrderCommand { get; set; }

		public OrderActionViewModel(DemoOrderDataViewModel? demoOrderDataViewModel, ICommand? backCommand, object? orderDisplayObj) : base(backCommand)
		{
			DemoOrderDataViewModel = demoOrderDataViewModel;
			var od = orderDisplayObj as OrderDisplayDTO;
			if (od == null) return;
			OrderDisplay = od;
			if (od.ODList != null && od.ODList.Count == 0)
			{
				od.Order = null;
				od.ODList = null;
			}
			if (od.ODList == null || od?.ODList.Count == 0)
                if (OrderDisplay.Table != null)
                    OrderDisplay.Table.Status = false;
            if (od?.Order != null)
				OrderDisplay.Order = od.Order;
			else
				OrderDisplay.Order = new OrderDTO();

			AddProductCommand = new RelayCommand(ExecuteAddProductCommand, CanExecuteAddProductCommand);
			AddQuantityCommand = new RelayCommand(ExecuteAddQuantityCommand);
			SubtractQuantityCommand = new RelayCommand(ExecuteSubtractQuantityCommand);
			PayOrderCommand = new RelayCommand(ExecutePayOrderCommand, CanExecutePayOrderCommand);
			//CancelOrderCommand = new RelayCommand(ExecuteCancelOrderCommand);
		}

		private bool CanExecutePayOrderCommand(object? obj)
		{
			if (OrderDisplay == null) return false;
			if (OrderDisplay.Order == null) return false;
			if (OrderDisplay.Table == null) return false;
			if (OrderDisplay.ODList == null) return false;
            if (OrderDisplay.ODList.Count == 0) return false;
			return true;
        }
        private void ExecutePayOrderCommand(object? obj)
		{
			string? msg = DemoOrderDataViewModel?.PayOrder(OrderDisplay);
			if (msg == null) msg = "Failed to pay the order";
			if (msg.Contains("successfully"))
				ExcuteSetNewOrder(null);
            System.Windows.MessageBox.Show(msg);
        }
		private bool CanExecuteAddProductCommand(object? obj)
		{
            var product = obj as ProductDTO;
			if (product == null) return false;
			if (product.Status == false) return false;
			return true;
        }
        private void ExecuteAddProductCommand(object? obj)
		{
			var product = obj as ProductDTO;
            if (product == null) return;
            if (OrderDisplay == null) return;
			if (OrderDisplay.ODList == null)
			{
				if (OrderDisplay.Table != null)
					OrderDisplay.Table.Status = true;
				if (OrderDisplay.Order != null)
				{
                    OrderDisplay.Order.OrderTime = DateTime.Now;
                    OrderDisplay.Order.TotalAmount = 0;
					OrderDisplay.Order.Description = string.Empty;
                    if (OrderDisplay.Table != null)
                        OrderDisplay.Order.TableID = OrderDisplay.Table.ID;
                }
                OrderDisplay.ODList = new ObservableCollection<OrderDetailDTO>();
			}
			OrderDisplay.ODList.Add(new OrderDetailDTO
			{
				ProductID = product.ID,
				ProductName = product.Name,
                UnitPrice = product.UnitPrice,
				Quantity = 1,
			}) ;
            GetTotalAmount();
        }
		private void ExecuteSubtractQuantityCommand(object? obj)
		{
			
            if (obj is OrderDetailDTO od && od.Quantity > 0)
            {

				if (od.Quantity != 1)
					od.Quantity--;
				else
					OrderDisplay?.ODList?.Remove(od);
				GetTotalAmount();
            }
        }
        private void ExecuteAddQuantityCommand(object? obj)
        {
            if (obj is OrderDetailDTO od)
            {
                od.Quantity ++;
				GetTotalAmount();
            }
        }
		private double GetTotalAmount()
		{
			double totalAmount = 0;
			if (OrderDisplay == null) return 0;
            if (OrderDisplay.ODList == null) return 0;
			foreach (var item in OrderDisplay.ODList)
			{
				totalAmount += item.UnitPrice * item.Quantity;
			}
			if (OrderDisplay.Order != null)
				OrderDisplay.Order.TotalAmount = totalAmount;
			if (totalAmount == 0)
				ExcuteSetNewOrder(null);
            return totalAmount;
		}
		private void ExcuteSetNewOrder(object? obj)
		{
			if (OrderDisplay == null) return;
			if (OrderDisplay.Table != null)
				OrderDisplay.Table.Status = false;
			if (OrderDisplay.Order != null)
			{
				OrderDisplay.Order.TotalAmount = 0;
				OrderDisplay.Order.OrderTime = DateTime.Now;
                OrderDisplay.Order.Description = string.Empty;
            }
            if (OrderDisplay.ODList != null)
			{
				OrderDisplay.ODList.Clear();
            }
		}






    }
}
