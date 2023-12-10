using DAL.Models;

namespace DAL.Interfaces
{
    public interface IProductRES
    {
        public bool Add(Product obj);
        public Product? AddnReturn(Product obj);
        public bool Update(Product obj);
        public bool Delete(Product obj);
        public List<Product> GetAll();
        public List<Product> GetByName(string name);
        public Product? CheckByName(string name);
        public Product? GetByID(int id);
    }
}
