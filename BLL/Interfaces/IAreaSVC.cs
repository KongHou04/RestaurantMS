using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
    public interface IAreaSVC
    {
        public string UpdateArea(AreaDTO obj);
        public List<AreaDTO> GetAllAreas();
        public List<AreaDTO> GetAllAreasByName(string name);
        public List<TableDTO> GetAllTables();
        public List<TableDTO> GetAllTablesByArea(AreaDTO obj);
        public string AddTables(AreaDTO obj);
        public string DeleteTables(AreaDTO obj);
    }
}
