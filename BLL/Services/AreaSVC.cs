using BLL.Interfaces;
using DAL.Interfaces;
using DAL.Models;
using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace BLL.Services
{
    public class AreaSVC : IAreaSVC
    {
        private readonly IAreaRES _areaRES;
        private readonly ITableRES _tableRES;

        public AreaSVC(IAreaRES areaRES, ITableRES tableRES)
        {
            _areaRES = areaRES;
            _tableRES = tableRES;
        }

        public string AddTables(AreaDTO obj)
        {
            var count = _areaRES.CountTableByID(obj.ID);
            if (count == 50)
                return "Sorry! 50 is maximize number you can reach. If you want more, please contact us!";
            if (_tableRES.Add(obj.ID))
                return "Add tables successfully";
            return "Cannot add new tables! Try later!";
        }

        public string DeleteTables(AreaDTO obj)
        {
            var count = _areaRES.CountTableByID(obj.ID);
            if (count == 0)
                return "Number is already reach 0!";
            if (_tableRES.Delete(obj.ID))
                return "Delete successfully";
            return "Cannot delete tables! Try later!";
        }

        public List<AreaDTO> GetAllAreas()
        {
            List<AreaDTO> entityList = new List<AreaDTO>();
            var list = _areaRES.GetAll();
            foreach (var item in list)
            {
                var tables = _areaRES.CountTableByID(item.AreaID);
                entityList.Add(new AreaDTO(item.AreaID)
                {
                    Name = item.Name?.Trim(),
                    Status = item.Status,
                    Description = item.Description,
                    TableNumber = tables,
                });
            }
            return entityList;
        }

        public List<AreaDTO> GetAllAreasByName(string name)
        {
            List<AreaDTO> entityList = new List<AreaDTO>();
            var list = _areaRES.GetByName(name);
            foreach (var item in list)
            {
                var tables = _areaRES.CountTableByID(item.AreaID);
                entityList.Add(new AreaDTO(item.AreaID)
                {
                    Name = item.Name?.Trim(),
                    Status = item.Status,
                    Description = item.Description,
                    TableNumber = tables,
                });
            }
            return entityList;
        }

        public List<TableDTO> GetAllTables()
        {
            List<TableDTO> list = new List<TableDTO>();
            var list1 = _tableRES.GetAll().Where(t => t.Status == true);
            var list2 = _areaRES.GetAll();
            list = (from t in list1
                    join r in list2
                    on t.AreaID equals r.AreaID
                    select new TableDTO()
                    {
                        ID = t.TableID,
                        Name = r.Name + t.TableOrdByArea,
                        OrderStatus = t.OrderStatus,
                        Status = t.Status,
                        AreaName = r.Name,
                        AreaID = r.AreaID,
                    }).ToList();

            return list == null ? new List<TableDTO>() : list;
        }

        public List<TableDTO> GetAllTablesByArea(AreaDTO obj)
        {
            List<TableDTO> list = new List<TableDTO>();
            var list1 = _tableRES.GetAll().Where(t => t.Status == true && t.AreaID == obj.ID && t.Area?.Status == true);
            var r = _areaRES.GetByID(obj.ID);
            list = (from t in list1
                    select new TableDTO()
                    {
                        ID = t.TableID,
                        Name = r?.Name + t.TableOrdByArea,
                        OrderStatus = t.OrderStatus,
                        Status = t.Status,
                        AreaName = r?.Name,
                        AreaID = r?.AreaID,
                    }).ToList();

            return list == null ? new List<TableDTO>() : list;
        }

        public string UpdateArea(AreaDTO obj)
        {
            var entity = _areaRES.GetByID(obj.ID);
            if (entity == null)
                return "Area does not exist!";
            if (obj.Name == null)
                return "Name cannot be empty";
            if (obj.Name.Length == 0)
                return "Name cannot be empty";
            if (obj.Name != entity.Name && _areaRES.CheckByName(obj.Name) != null)
                return "Area name already exist";
            entity.Name = obj.Name;
            entity.Status = obj.Status;
            entity.Description = obj.Description;
            if (_areaRES.Update(entity))
                return "Update sucessfully";
            else
                return "Cannot update the area";
        }
    }
}
