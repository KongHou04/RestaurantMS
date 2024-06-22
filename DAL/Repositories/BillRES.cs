using DAL.Contexts;
using DAL.Models;
using DAL.Repositories;

namespace DAL.Interfaces
{
    public class BillRES : BaseRES, IBillRES
    {
        public BillRES(RMContext context) : base(context) { }

        public bool Add(Bill entity)
        {
            throw new NotImplementedException();
        }

        public bool AddByOrderID(int objID)
        {
            var o = _context.Orders.FirstOrDefault(od => od.OrderID == objID);
            if (o == null) return false;
            var bill = new Bill()
            {
                CreateTime = DateTime.Now.ToUniversalTime(),
                GrandTotal = o.TotalAmount,
                OrderID = o.OrderID,
            };
            try
            {
                _context.Bills.Add(bill);
                _context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        public bool Delete(Bill entity)
        {
            throw new NotImplementedException();
        }

        public List<Bill> GetAll()
        {
            throw new NotImplementedException();
        }

        public List<Bill> GetByDate(DateTime date)
        {

            // Bugs
            //return _context.Bills.Where(b => b.CreateTime.Date == date.ToUniversalTime()).ToList();
            return _context.Bills.ToList();
        }

        public Bill GetByID(int id)
        {
            throw new NotImplementedException();
        }

        public List<Bill> GetByOrder(int orderID)
        {
            throw new NotImplementedException();
        }

        public bool Update(Bill entity)
        {
            throw new NotImplementedException();
        }
    }
}
