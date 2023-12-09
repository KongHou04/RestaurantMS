using DAL.Models;

namespace DAL.Interfaces
{
    public interface ICategoryRES
    {
        public bool Add(Category obj);
        public bool Update(Category obj);
        public bool Delete(Category obj);
        public List<Category> GetAll();
        public List<Category> GetByName(string name);
        public Category? CheckByName(string name);
        public Category? GetByID(int id);
    }
}
