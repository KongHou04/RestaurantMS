using BLL.Interfaces;
using DAL.Interfaces;
using DAL.Models;
using DTO;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.InteropServices.ObjectiveC;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class OrderSVC : IOrderSVC
    {
        private readonly ITableRES _tableRES;
        private readonly IAreaRES _areaRES;
        private readonly IOrderRES _orderRES;
        private readonly IOrderDetailRES _orderDetailRES;
        private readonly IBillRES _billRES;


        public OrderSVC(ITableRES tableRES, IAreaRES areaRES, IOrderRES orderRES, IOrderDetailRES orderDetailRES, IBillRES billRES)
        {
            _tableRES = tableRES;
            _areaRES = areaRES;
            _orderRES = orderRES;
            _orderDetailRES = orderDetailRES;
            _billRES = billRES;
        }

        public object? GetOrderByTable(TableDTO obj)
        {
            var ord = _orderRES.GetCurrentOrderByTable(obj.ID);
            return ord;
        }

        public List<TableDTO> GetAllValidTablesByArea(AreaDTO? obj)
        {
            List<TableDTO> list = new List<TableDTO>();
            List<Table> list1 = new List<Table>();
            List<Area> list2 = new List<Area>();
            if (obj == null)
            {
                list1 = _tableRES.GetAll().Where(t => t.Status == true/* && t.AreaID == obj.ID*/).ToList();
                list2 = _areaRES.GetAll();
                list = (from t in list1
                        join r in list2
                        on t.AreaID equals r.AreaID
                        select new TableDTO
                        {
                            ID = t.TableID,
                            Name = r.Name + t.TableOrdByArea,
                            OrderStatus = t.OrderStatus,
                            Status = t.Status,
                            AreaName = r.Name,
                            AreaID = r.AreaID,
                        }).ToList();
            }
            else
            {
                list1 = _tableRES.GetAll().Where(t => t.Status == true && t.AreaID == obj.ID).ToList();
                var r = _areaRES.GetByID(obj.ID);
                if (r != null)
                    list = (from t in list1
                            select new TableDTO
                            {
                                ID = t.TableID,
                                Name = r.Name + t.TableOrdByArea,
                                OrderStatus = t.OrderStatus,
                                Status = t.Status,
                                AreaName = r.Name,
                                AreaID = r.AreaID,
                            }).ToList();
            }
            return list;
        }

        public List<TableDTO> GetAllValidTables()
        {
            List<TableDTO> list = new List<TableDTO>();
            var list1 = _tableRES.GetAll().Where(t => t.Status == true && t.Area?.Status == true);
            var list2 = _areaRES.GetAllValid();

            list = (from t in list1
                    join r in list2
                    on t.AreaID equals r.AreaID
                    select new TableDTO()
                    {
                        ID = t.TableID,
                        Name = r?.Name + t.TableOrdByArea,
                        OrderStatus = t.OrderStatus,
                        Status = t.Status,
                        AreaName = r?.Name,
                        AreaID = r?.AreaID,
                    }).ToList();
            return list;
        }

        public List<AreaDTO> GetAllValidAreas()
        {
            return _areaRES.GetAllValid().Select(item => new AreaDTO()
            {
                ID = item.AreaID,
                Name = item.Name?.Trim(),
                Status = item.Status,
                Description = item.Description,
            }).ToList();
        }

        public OrderDisplayDTO? GetOrderDisplay(TableDTO? obj)
        {
            if (obj == null) return null;
            var table = _tableRES.GetByID(obj.ID);
            if (table == null) return null;
            var order = _orderRES.GetCurrentOrderByTable(obj.ID);
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

        public string UpdateOrderByOrderDisPlay(OrderDisplayDTO? obj)
        {
            if (obj == null) return "Order is empty";
            if (obj.Table == null) return "No Table choosed";
            if (obj.Order == null) return "Order information is empty";
            if (obj.ODList == null) return "No Table choosed";

            var ord = new Order
            {
                OrderTime = obj.Order.OrderTime,
                TotalAmount = obj.Order.TotalAmount,
                TableID = obj.Table.ID,
                Status = true,
                PaymentStatus = false,
            };

            if (obj.Order.ID == 0)
            {
                var o = _orderRES.AddnReturn(ord);
                if (o == null) return "Cannot add order! Try later";
                foreach (var item in obj.ODList)
                {
                    item.OrderID = o.OrderID;
                }
            }
            else
                _orderRES.Update(ord);

            foreach (var item in obj.ODList)
            {
                var od = new OrderDetail
                {
                    ODID = item.ID,
                    OrderID = item.OrderID,
                    ProductID = item.ProductID,
                };
                if (item.ID == 0)
                    _orderDetailRES.Add(od);
                else
                    _orderDetailRES.Update(od);
            }
            return string.Empty;
        }


        public string PayOrder(OrderDisplayDTO? orderDisplayDTO)
        {
            if (orderDisplayDTO == null) return "Order is empty";
            if (orderDisplayDTO.ODList == null) return "Order is empty";
            if (orderDisplayDTO.Order == null) return "Order is empty";
            var o = new Order()
            {
                TableID = orderDisplayDTO.Table?.ID,
                OrderTime = orderDisplayDTO.Order.OrderTime.ToUniversalTime(),
                Description = orderDisplayDTO.Order.Description,
                TotalAmount = orderDisplayDTO.Order.TotalAmount,
                GrandTotal = orderDisplayDTO.Order.TotalAmount,
                Status = true,
            };
            o = _orderRES.AddnReturn(o);
            if (o == null) return "Error to save the order";
            foreach (var item in orderDisplayDTO.ODList)
            {
                var od = new OrderDetail()
                {
                    OrderID = o.OrderID,
                    ProductName = item.ProductName,
                    ProductID = item.ProductID,
                    Quantity = item.Quantity,
                };
                _orderDetailRES.Add(od);
            }
            if (_billRES.AddByOrderID(o.OrderID))
                return "Pay successfully";
            return "Cannot pay the order";

        }
    }
}
