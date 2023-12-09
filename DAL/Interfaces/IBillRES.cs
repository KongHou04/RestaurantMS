using DAL.Models;

namespace DAL.Interfaces
{
    public interface IBillRES
    {
        public bool Add(Bill obj);
        public bool Update(Bill obj);
        public bool Delete(Bill obj);
        public List<Bill> GetAll();
        public List<Bill> GetByOrder(int orderID);
        public List<Bill> GetByDate(DateTime date);
        public Bill? GetByID(int id);
    }
}
