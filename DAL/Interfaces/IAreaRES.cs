using DAL.Models;

namespace DAL.Interfaces
{
    public interface IAreaRES
    {
        public bool Add(Area obj);
        public bool Update(Area obj);
        public bool Delete(Area obj);
        public List<Area> GetAll();
        public List<Area> GetAllValid();
        public List<Area> GetByName(string name);
        public Area? GetByID(int id);
        public Area? CheckByName(string name);
        public int CountTableByID(int id);
    }
}
