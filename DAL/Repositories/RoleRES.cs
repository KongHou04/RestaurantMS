using DAL.Contexts;
using DAL.Models;
using DAL.Repositories;

namespace DAL.Interfaces
{
    public class RoleRES : BaseRES, IRoleRES
    {
        public RoleRES(RMContext context) : base(context) { }

        public List<Role> GetAll()
        {
            return _context.Roles.ToList();
        }

        public Role? GetByID(int id)
        {
            return _context.Roles.ToList().FirstOrDefault(o => o.RoleID == id);
        }
    }
}
