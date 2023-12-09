using DAL.Models;

namespace DAL.Interfaces
{
    public interface IOrderDetailRES
    {
        public bool Add(OrderDetail obj);
        public bool Update(OrderDetail obj);
        public bool Delete(OrderDetail obj);
        public List<OrderDetail> GetByOrder(int orderID);
        public OrderDetail? GetByID(int id);
        public void UpdateTotalAmount(int orderID);
    }
}
