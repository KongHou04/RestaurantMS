using DAL.Contexts;
using DAL.Models;
using DAL.Repositories;

namespace DAL.Interfaces
{
    public class CategoryRES : BaseRES, ICategoryRES
    {
        public CategoryRES(RMContext context) : base(context) { }

        public bool Add(Category obj)
        {
            Category category = new Category();
            try
            {
                category.Name = obj.Name?.Trim();
                category.Description = obj.Description;
                category.Status = obj.Status;
                _context.Add(category);
                _context.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
        }

        public Category? CheckByName(string name)
        {
            return _context.Categories.ToList().FirstOrDefault(o => o.Name?.ToLower() == name.ToLower());
        }

        public bool Delete(Category obj)
        {
            var deleteObj = GetByID(obj.CategoryID);
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

        public List<Category> GetAll()
        {
            return _context.Categories.ToList();
        }

        public Category? GetByID(int id)
        {
            return _context.Categories.ToList().FirstOrDefault(c => c.CategoryID == id);
        }

        public List<Category> GetByName(string name)
        {
            return _context.Categories.OrderBy(o => o.CategoryID).Where(c => c.Name != null && c.Name.ToLower().Contains(name.ToLower())).ToList();
        }

        public bool Update(Category obj)
        {
            var updateObj = GetByID(obj.CategoryID);
            if (updateObj == null)
                return false;
            try
            {
                updateObj.Name = obj.Name;
                updateObj.Description = obj.Description;
                updateObj.Status = obj.Status;
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
