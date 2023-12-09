using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
    public interface IEmployeeSVC
    {
        public string Add(EmployeeDTO obj);
        public string Update(EmployeeDTO obj);
        public string Delete(int objID);

        public List<EmployeeDTO> GetByName(string name);


    }
}
