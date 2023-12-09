using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
    public interface IUserSVC
    {
        public EmployeeDTO? Login(string username, string password);
        public bool UpdatePassword(string email, string newPassword);
        public string? GetUserNameByEmail(string email);

    }
}
