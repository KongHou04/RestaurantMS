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
            throw new NotImplementedException();
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
