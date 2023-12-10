using BLL.Interfaces;
using DAL.Interfaces;
using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class SelfUserSVC : ISelfUserSVC
    {
        public ImageHandlerSVC _imageHandlerSVC;
        private readonly IAccountRES _accountRES;
        private readonly IEmployeeRES _employeeRES;
        private readonly IRoleRES _roleRES;

        public SelfUserSVC(IAccountRES accountRES, IEmployeeRES employeeRES, IRoleRES roleRES, ImageHandlerSVC imageHandlerSVC)
        {
            _accountRES = accountRES;
            _employeeRES = employeeRES;
            _roleRES = roleRES;
            _imageHandlerSVC = imageHandlerSVC;
        }

        public EmployeeDTO? RefreshCurrentUser(EmployeeDTO employee)
        {
            if (employee.Email == null) return null;
            var e = _employeeRES.GetByEmail(employee.Email);
            if (e == null) return null;
            var eDTO = new EmployeeDTO()
            {
                ID = e.EmployeeID,
                FullName = e.FullName,
                BirthDate = e.BirthDate,
                Email = e.Email,
                Phone = e.Phone,
                Status = e.Status,
                Avatar = _imageHandlerSVC.GetImageDirecory(e.Avatar, true),
                RoleName = e?.Role?.Name,
                RoleID = e?.Role?.RoleID,
                UserName = e?.Account?.UserName,
                AvatarBitMap = _imageHandlerSVC.GetBitMap(e?.Avatar, true)
            };
            return eDTO;
        }
    }
}
