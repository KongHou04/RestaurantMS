using DAL.Contexts;
using DAL.Models;
using DAL.Repositories;

namespace DAL.Interfaces
{
    public class AreaRES : BaseRES, IAreaRES
    {
        public AreaRES(RMContext context) : base(context) { }

        public bool Add(Area obj)
        {
            Area Area = new Area();
            try
            {
                Area.Name = obj.Name?.Trim();
                Area.Description = obj.Description;
                Area.Status = obj.Status;
                _context.Add(Area);
                _context.SaveChanges();
                return true;

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
        }

        public Area? CheckByName(string name)
        {
            return _context.Areas.ToList().FirstOrDefault(o => o.Name?.ToLower() == name.ToLower());
        }

        public int CountTableByID(int id)
        {
            return _context.Tables.Where(t => t.AreaID == id && t.Status == true).Count();
        }

        public bool Delete(Area obj)
        {
            var deleteObj = GetByID(obj.AreaID);
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

        public List<Area> GetAll()
        {
            return _context.Areas.OrderBy(o => o.AreaID).ToList();
        }

        public List<Area> GetAllValid()
        {
            return _context.Areas.Where(o => o.Status == true).OrderBy(o => o.AreaID).ToList();
        }

        public Area? GetByID(int id)
        {   
            return _context.Areas.ToList().FirstOrDefault(o => o.AreaID == id);
        }

        public List<Area> GetByName(string name)
        {
            return _context.Areas.Where(o => o.Name != null && o.Name.ToLower().Contains(name.ToLower())).OrderBy(o => o.AreaID).ToList();
        }

        public bool Update(Area obj)
        {
            var updateObj = GetByID(obj.AreaID);
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
