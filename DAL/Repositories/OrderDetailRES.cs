using DAL.Contexts;
using DAL.Models;
using DAL.Repositories;

namespace DAL.Interfaces
{
    public class OrderDetailRES : BaseRES, IOrderDetailRES
    {
        public OrderDetailRES(RMContext context) : base(context) { }

        public bool Add(OrderDetail obj)
        {
            OrderDetail orderDetail = new OrderDetail();
            try
            {
                orderDetail.Quantity = obj.Quantity;
                orderDetail.Description = obj.Description;
                orderDetail.OrderID = obj.OrderID;
                orderDetail.ProductID = obj.ProductID;
                var pro = _context.Products.FirstOrDefault(p => p.ProductID == obj.ProductID);
                if (pro == null)
                    throw new Exception("Product does not exist");
                orderDetail.Price = pro.UnitPrice;
                orderDetail.ProductName = pro.Name;
                //if (orderDetail.OrderID != null)
                //    UpdateTotalAmount((int)orderDetail.OrderID);
                _context.Add(orderDetail);
                _context.SaveChanges();
                return true;

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
        }

        public bool Delete(OrderDetail obj)
        {
            var deleteObj = GetByID(obj.ODID);
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

        public OrderDetail? GetByID(int id)
        {
            return _context.OrderDetails.ToList().FirstOrDefault(o => o.ODID == id);
        }

        public List<OrderDetail> GetByOrder(int orderID)
        {
            return _context.OrderDetails.ToList().Where(o => o.OrderID == orderID).ToList();
        }

        public bool Update(OrderDetail obj)
        {
            var updateObj = GetByID(obj.ODID);
            if (updateObj == null)
                return false;
            try
            {
                updateObj.Quantity = obj.Quantity;
                updateObj.Description = obj.Description;
                updateObj.OrderID = obj.OrderID;
                updateObj.ProductID = obj.ProductID;
                if (updateObj.ProductID != obj.ProductID)
                {
                    var pro = _context.Products.FirstOrDefault(p => p.ProductID == obj.ProductID);
                    if (pro == null)
                        throw new Exception("Product does not exist");
                    updateObj.Price = pro.UnitPrice;
                    updateObj.ProductName = pro.Name;
                }
                if (updateObj.OrderID != null)
                    UpdateTotalAmount((int)updateObj.OrderID);
                _context.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
        }
        public void UpdateTotalAmount(int orderID)
        {
            double totalAmount = 0;
            var ord = _context.Orders.ToList().FirstOrDefault(o => o.OrderID == orderID);
            if (ord == null)
                return;
            List<OrderDetail> ODList = _context.OrderDetails.ToList().Where(od => od.OrderID == ord.OrderID).ToList();
            foreach (var od in ODList)
                totalAmount += od.Quantity * od.Quantity;
            ord.TotalAmount = totalAmount;
            ord.UpdateGrandTotal();
        }
    }
}
