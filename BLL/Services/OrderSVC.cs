using BLL.Interfaces;
using DAL.Interfaces;
using DAL.Models;
using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
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


        public OrderSVC(ITableRES tableRES, IAreaRES areaRES, IOrderRES orderRES, IOrderDetailRES orderDetailRES)
        {
            _tableRES = tableRES;
            _areaRES = areaRES;
            _orderRES = orderRES;
            _orderDetailRES = orderDetailRES;
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
    }
}
