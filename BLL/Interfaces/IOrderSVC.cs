using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
    public interface IOrderSVC
    {
        public List<TableDTO> GetAllValidTables();
        public List<TableDTO> GetAllValidTablesByArea(AreaDTO? obj);
        public object? GetOrderByTable(TableDTO obj);
        public List<AreaDTO> GetAllValidAreas();
        //public object? GetOrderByTable(TableDTO obj);




    }
}
