using DAL.Contexts;
using DAL.Models;
using DAL.Repositories;

namespace DAL.Interfaces
{
    public class EmployeeRES : BaseRES, IEmployeeRES
    {
        public EmployeeRES(RMContext context) : base(context) { }

        public bool Add(Employee obj)
        {
            Employee user = new Employee();
            try
            {
                user.FullName = obj.FullName;
                user.BirthDate = obj.BirthDate;
                user.Email = obj.Email;
                user.Phone = obj.Phone;
                user.Avatar = obj.Avatar;
                user.Status = obj.Status;
                user.RoleID = obj.RoleID;
                _context.Add(user);
                _context.SaveChanges();
                return true;

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
        }

        public Employee? AddnReturn(Employee obj)
        {
            Employee user = new Employee();
            try
            {
                user.FullName = obj.FullName;
                user.BirthDate = obj.BirthDate;
                user.Email = obj.Email;
                user.Phone = obj.Phone;
                user.Avatar = obj.Avatar;
                user.Status = obj.Status;
                user.RoleID = obj.RoleID;
                _context.Employees.Add(user);
                _context.SaveChanges();
                return user;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
        }

        public bool Delete(Employee obj)
        {
            var deleteObj = GetByID(obj.EmployeeID);
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

        public List<Employee> GetAll()
        {
            return _context.Employees.ToList();
        }

        public Employee? GetByEmail(string email)
        {
            return _context.Employees.ToList().FirstOrDefault(o => o.Email != null && o.Email.ToLower() == email.ToLower());
        }

        public Employee? GetByID(int id)
        {
            return _context.Employees.ToList().FirstOrDefault(o => o.EmployeeID == id);
        }

        public List<Employee> GetByName(string name)
        {
            return _context.Employees.ToList().Where(o => o.FullName != null && o.FullName.Contains(name)).ToList();
        }

        public Employee? GetByPhone(string phone)
        {
            return _context.Employees.ToList().FirstOrDefault(o => o.Email != null && o.Email.ToLower() == phone.ToLower());
        }

        public bool Update(Employee obj)
        {
            var updateObj = GetByID(obj.EmployeeID);
            if (updateObj == null)
                return false;
            try
            {
                updateObj.FullName = obj.FullName;
                updateObj.BirthDate = obj.BirthDate;
                updateObj.Email = obj.Email;
                updateObj.Phone = obj.Phone;
                updateObj.Avatar = obj.Avatar;
                updateObj.Status = obj.Status;
                updateObj.RoleID = obj.RoleID;
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
