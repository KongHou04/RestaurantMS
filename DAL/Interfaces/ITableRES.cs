using DAL.Models;

namespace DAL.Interfaces
{
    public interface ITableRES
    {
        public bool Add(int regionID, int number = 25);
        public bool Delete(int regionID, int number = 25);
        public bool Update(Table obj);
        public Table? GetByID(int id);
        public List<Table> GetAll();
        public List<Table> GetByAreaID(int id);
    }
}
