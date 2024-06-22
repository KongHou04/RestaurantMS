using BLL.Interfaces;
using DAL.Interfaces;
using DTO;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class HistorySVC : IHistorySVC
    {
        private readonly ITableRES _tableRES;
        private readonly IAreaRES _areaRES;
        private readonly IOrderRES _orderRES;
        private readonly IOrderDetailRES _orderDetailRES;
        private readonly IBillRES _billRES;

        public HistorySVC(ITableRES tableRES, IAreaRES areaRES, IOrderRES orderRES, IOrderDetailRES orderDetailRES, IBillRES billRES)
        {
            _tableRES = tableRES;
            _areaRES = areaRES;
            _orderRES = orderRES;
            _orderDetailRES = orderDetailRES;
            _billRES = billRES;
        }

        public List<BillDTO> GetAllHistoryByDate(DateTime date)
        {
            var l = _billRES.GetByDate(date).Select(o => new BillDTO
            {
                GrandTotal = o.GrandTotal,
                CreateTime = o.CreateTime,
                Recived = o.GrandTotal,
                OrderID = o.OrderID,
            }).ToList();
            return l;
        }

        public OrderDisplayDTO? GetHistoryByBill(BillDTO? obj)
        {
            if (obj == null) return null;
            var bill = _billRES.GetByID(obj.ID);
            if (bill == null) return null;
            if (bill.OrderID == null) return null;
            var order = _orderRES.GetCurrentOrderByTable((int)bill.OrderID);
            if (order == null) return null;
            var odList = _orderDetailRES.GetByOrder(order.OrderID);
            var tableDTO = new TableDTO
            {
                ID = obj.ID,
            };
            var orderDTO = new OrderDTO()
            {
                ID = order.OrderID,
                Description = order.Description,
                OrderTime = order.OrderTime,
                GrandTotal = order.GrandTotal,
            };
            var odDTOList = (from od in odList
                             select new OrderDetailDTO
                             {
                                 ProductName = od.ProductName,
                                 Quantity = od.Quantity,
                                 UnitPrice = od.Price,
                             }).ToList();
            return new OrderDisplayDTO()
            {
                Order = orderDTO,
                ODList = new ObservableCollection<OrderDetailDTO>(odDTOList),
                Table = tableDTO,
            };

        }
    }
}
