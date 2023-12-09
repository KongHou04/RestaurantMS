using BLL.Interfaces;
using DAL.Interfaces;
using DAL.Models;
using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class EmployeeSVC : IEmployeeSVC
    {
        private IAccountRES _accountRES;
        private IEmployeeRES _employeeRES;
        private IRoleRES _roleRES;
        public EmployeeSVC(IAccountRES accountRES, IEmployeeRES employeeRES, IRoleRES roleRES)
        {
            _accountRES = accountRES;
            _employeeRES = employeeRES;
            _roleRES = roleRES;
        }


        public string Add(EmployeeDTO obj)
        {
            if (obj.FullName == null || obj.UserName == null)
                return "Name/Username cannot be empty";
            if (obj.Email == null || obj.Phone == null)
                return "Name/Email cannot be empty";
            if (_employeeRES.GetByEmail(obj.Email) != null)
                return "Email already exist";
            if (_employeeRES.GetByPhone(obj.Phone) != null)
                return "Email already exist";
            var entity = new Employee()
            {
                FullName = obj.FullName?.Trim(),
                BirthDate = obj.BirthDate,
                Email = obj.Email,
                Phone = obj.Phone,
                Avatar = obj.Avatar,
                Status = obj.Status,
                RoleID = obj.RoleID,
            };
            if (_employeeRES.Add(entity))
                return "Add successfully";
            return "Cannot add new employee! Try later!";
        }

        public string Delete(int objID)
        {
            throw new NotImplementedException();
        }

        public List<EmployeeDTO> GetByName(string name)
        {
            var list1 = _employeeRES.GetByName(name);
            var list2 = _roleRES.GetAll();
            var list = from e in list1
                       join r in list2
                       on e.RoleID equals r.RoleID
                       select new EmployeeDTO()
                       {
                           FullName = e.FullName,
                           BirthDate = e.BirthDate,
                           Email = e.Email,
                           Phone = e.Phone,
                           Status = e.Status,
                           Avatar = e.Avatar,
                           RoleName = r.Name,
                           RoleID = r.RoleID,
                           //UserName = (_accountRES.GetByEmpID(e.EmployeeID))?.UserName,
                           UserName = e.Account?.UserName
                       };
            return list.ToList();
        }

        public string Update(EmployeeDTO obj)
        {
            var updateObj = _employeeRES.GetByID(obj.ID);
            if (updateObj == null)
                return "Employee does not exist!";
            updateObj.FullName = obj.FullName;
            updateObj.BirthDate = obj.BirthDate;
            updateObj.Email = obj.Email;
            updateObj.Phone = obj.Phone;
            updateObj.Status = obj.Status;
            updateObj.Avatar = obj.Avatar;
            updateObj.RoleID = obj.RoleID;
            if (_employeeRES.Update(updateObj))
                return "Update Successfully";
            else
                return "Cannot update the employee";
        }
    }
}
