using DAL.Contexts;
using DAL.Models;
using DAL.Repositories;

namespace DAL.Interfaces
{
    public class OrderRES : BaseRES, IOrderRES
    {
        public OrderRES(RMContext context) : base(context) { } 

        public bool Add(Order obj)
        {
            Order order = new Order();
            try
            {
                order.OrderTime = obj.OrderTime;
                order.Incedentals = obj.Incedentals;
                order.Discount = obj.Discount;
                order.Status = obj.Status;
                order.PaymentStatus = obj.PaymentStatus;
                order.Description = obj.Description;
                order.TableID = obj.TableID;
                order.EmployeeID = obj.EmployeeID;
                order.UpdateGrandTotal();
                _context.SaveChanges();
                return true;

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
        }

        public bool Delete(Order obj)
        {
            var deleteObj = GetByID(obj.OrderID);
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

        public List<Order> GetAll()
        {
            return _context.Orders.ToList();
        }

        public List<Order> GetByDate(DateTime date)
        {
            return _context.Orders.ToList().Where(o => o.OrderTime.Date == date.Date).ToList();
        }

        public Order? GetByID(int id)
        {
            return _context.Orders.ToList().FirstOrDefault(c => c.OrderID == id);
        }

        public Order? GetLastByTable(int tableID)
        {
            return _context.Orders.ToList().LastOrDefault(c => c.TableID == tableID);
        }

        public bool Update(Order obj)
        {
            var updateObj = GetByID(obj.OrderID);
            if (updateObj == null)
                return false;
            try
            {
                updateObj.OrderTime = obj.OrderTime;
                updateObj.Incedentals = obj.Incedentals;
                updateObj.Discount = obj.Discount;
                updateObj.Status = obj.Status;
                updateObj.PaymentStatus = obj.PaymentStatus;
                updateObj.Description = obj.Description;
                updateObj.TableID = obj.TableID;
                updateObj.EmployeeID = obj.EmployeeID;
                updateObj.UpdateGrandTotal();
                _context.Add(updateObj);
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
