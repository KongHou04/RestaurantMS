using DAL.Models;

namespace DAL.Interfaces
{
    public interface IEmployeeRES
    {
        public bool Add(Employee obj);
        public Employee? AddnReturn(Employee obj);
        public bool Update(Employee obj);
        public bool Delete(Employee obj);
        public List<Employee> GetAll();
        public List<Employee> GetByName(string name);
        public Employee? GetByEmail(string email);
        public Employee? GetByPhone(string phone);
        public Employee? GetByID(int id);
    }
}
