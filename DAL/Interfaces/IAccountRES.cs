using DAL.Models;

namespace DAL.Interfaces
{
    public interface IAccountRES
    {
        public bool Add(Account obj);
        public bool Update(Account obj);
        public bool Delete(Account obj);
        public List<Account> GetByName(string name);
        public Account? GetByUserName(string username);
        public Account? GetByEmpID(int empID);
    }
}
