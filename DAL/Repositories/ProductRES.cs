using DAL.Contexts;
using DAL.Interfaces;
using DAL.Models;

namespace DAL.Repositories
{
    public class ProductRES : BaseRES, IProductRES
    {
        public ProductRES(RMContext context) : base(context) { }

        public bool Add(Product obj)
        {
            Product product = new Product();
            try
            {
                product.Name = obj.Name?.Trim();
                product.UnitPrice = obj.UnitPrice;
                product.Image = obj.Image;
                product.Description = obj.Description;
                product.Status = obj.Status;
                product.CategoryID = obj.CategoryID;
                _context.Add(product);
                _context.SaveChanges();
                return true;

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
        }

        public Product? AddnReturn(Product obj)
        {
            Product product = new Product();
            try
            {
                product.Name = obj.Name?.Trim();
                product.UnitPrice = obj.UnitPrice;
                product.Image = obj.Image;
                product.Description = obj.Description;
                product.Status = obj.Status;
                product.CategoryID = obj.CategoryID;
                _context.Products.Add(product);
                _context.SaveChanges();
                return product;

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
        }

        public Product? CheckByName(string name)
        {
            return _context.Products.ToList().FirstOrDefault(o => o.Name?.ToLower() == name.ToLower());
        }

        public bool Delete(Product obj)
        {
            var deleteObj = GetByID(obj.ProductID);
            if (deleteObj == null)
                return false;
            try
            {
                _context.Remove(deleteObj);
                _context.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
        }

        public List<Product> GetAll()
        {
            return _context.Products.ToList();
        }

        public Product? GetByID(int id)
        {
            return _context.Products.ToList().FirstOrDefault(o => o.ProductID == id);
        }

        public List<Product> GetByName(string name)
        {
            return _context.Products.Where(o => o.Category != null && o.Name != null && o.Name.ToLower().Contains(name.ToLower()) == true).ToList();
        }

        public bool Update(Product obj)
        {
            var updateObj = GetByID(obj.ProductID);
            if (updateObj == null)
                return false;
            try
            {
                updateObj.Name = obj.Name;
                updateObj.UnitPrice = obj.UnitPrice;
                updateObj.Image = obj.Image;
                updateObj.Description = obj.Description;
                updateObj.Status = obj.Status;
                updateObj.CategoryID = obj.CategoryID;
                _context.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
        }
    }
}
