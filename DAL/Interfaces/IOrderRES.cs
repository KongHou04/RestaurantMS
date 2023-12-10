using DAL.Models;

namespace DAL.Interfaces
{
    public interface IOrderRES
    {
        public bool Add(Order obj);
        public bool Update(Order obj);
        public bool Delete(Order obj);
        public List<Order> GetAll();
        public List<Order> GetAllUnpayOrder();
        public Order? GetCurrentOrderByTable(int tableID);
        public List<Order> GetByDate(DateTime date);
        public Order? GetByID(int id);
    }
}
