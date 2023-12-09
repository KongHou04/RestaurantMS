using DAL.Contexts;
using DAL.Models;
using DAL.Repositories;

namespace DAL.Interfaces
{
    public class TableRES : BaseRES, ITableRES
    {
        public TableRES(RMContext context) : base(context) { }

        public bool Add(int AreaID, int number = 25)
        {
            int n = new int();
            var obj = _context.Areas.FirstOrDefault(c => c.AreaID == AreaID);
            if (obj == null) 
                return false;
            var lastObj = _context.Tables.OrderBy(t => t.TableID).LastOrDefault(t => t.AreaID == obj.AreaID && t.Status == true);
            if (lastObj == null)
            {
                lastObj = _context.Tables.OrderBy(t => t.TableID).FirstOrDefault(t => t.AreaID == obj.AreaID);
                if (lastObj == null)
                    return false;
                n = lastObj.TableID - 1;
            }
            else
                n = lastObj.TableID;
            for (int i = n; i < n + number; i++)
            {
                var table = _context.Tables.FirstOrDefault(t => t.TableID == i + 1);
                if (table != null)
                {
                    table.Status = true;
                    _context.Update(table);
                }
            }
            try
            {
                _context.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
        }

        public bool Delete(int AreaID, int number = 25)
        {
            var obj = _context.Areas.ToList().FirstOrDefault(c => c.AreaID == AreaID);
            if (obj == null)
                return false;
            var lastObj = _context.Tables.OrderBy(t => t.TableID).LastOrDefault(t => t.AreaID == obj.AreaID && t.Status == true);
            if (lastObj == null)
                return false;
            for (int i = lastObj.TableID - number; i < lastObj.TableID; i++)
            {
                var table = _context.Tables.FirstOrDefault(t => t.TableID == i + 1);
                if (table != null)
                {
                    table.Status = false;
                    _context.Update(table);
                }
            }
            try
            {
                _context.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
        }

        public List<Table> GetAll()
        {
            return _context.Tables.OrderBy(o => o.TableID).ToList();
        }

        public List<Table> GetByAreaID(int id)
        {
            return _context.Tables.Where(o => o.AreaID == id).OrderBy(o => o.TableID).ToList();
        }

        public Table? GetByID(int id)
        {
            return _context.Tables.FirstOrDefault(t => t.TableID == id);
        }

        public bool Update(Table entity)
        {
            var tab = GetByID(entity.TableID);
            if (tab == null)
                return false;
            try
            {
                tab.Status = entity.Status;
                //tab.Status = entity.Status;
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
