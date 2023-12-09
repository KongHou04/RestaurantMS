using DAL.Models;

namespace DAL.Interfaces
{
    public interface IRoleRES
    {
        public List<Role> GetAll();
        public Role? GetByID(int id);
    }
}
