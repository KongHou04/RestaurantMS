using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
    public interface ICategorySVC
    {
        public string Add(CategoryDTO obj);
        public string Update(CategoryDTO obj);
        public string Delete(int objID);
        public List<CategoryDTO> GetAll();
        public List<CategoryDTO> GetByName(string name);

    }
}
