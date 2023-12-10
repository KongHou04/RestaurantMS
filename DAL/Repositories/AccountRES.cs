using DAL.Contexts;
using DAL.Models;
using DAL.Repositories;

namespace DAL.Interfaces
{
    public class AccountRES : BaseRES, IAccountRES
    {
        public AccountRES(RMContext context) : base(context) { }

        public bool Add(Account obj)
        {
            Account account = new Account();
            try
            {
                account.UserName = obj.UserName;
                account.HashPassword = obj.HashPassword;
                account.EmployeeID = obj.EmployeeID;
                _context.Add(account);
                _context.SaveChanges();
                return true;

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
        }

        public bool Delete(Account obj)
        {
            throw new NotImplementedException();
        }

        public Account? GetByEmpID(int empID)
        {
            return _context.Accounts.ToList().FirstOrDefault(o => o.EmployeeID == empID);
        }

        public List<Account> GetByName(string name)
        {
            return _context.Accounts.ToList().Where(o => o.UserName != null && o.UserName.Contains(name)).ToList();
        }

        public Account? GetByUserName(string username)
        {
            return _context.Accounts.ToList().FirstOrDefault(o => o.UserName == username);
        }

        public bool Update(Account obj)
        {
            if (obj.UserName == null)
                return false;
            var updateObj = GetByUserName(obj.UserName);
            if (updateObj == null)
                return false;
            try
            {
                updateObj.UserName = obj.UserName;
                updateObj.HashPassword = obj.HashPassword;
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
