using BLL.Interfaces;
using DAL.Interfaces;
using DAL.Models;
using DAL.Repositories;
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
        public ImageHandlerSVC _imageHandlerSVC;
        private readonly IAccountRES _accountRES;
        private readonly IEmployeeRES _employeeRES;
        private readonly IRoleRES _roleRES;
        public EmployeeSVC(IAccountRES accountRES, IEmployeeRES employeeRES, IRoleRES roleRES, ImageHandlerSVC imageHandlerSVC)
        {
            _accountRES = accountRES;
            _employeeRES = employeeRES;
            _roleRES = roleRES;
            _imageHandlerSVC = imageHandlerSVC;
        }


        public string Add(EmployeeDTO obj)
        {
            string? msg = CheckInputData.CheckValidInpurtForEmployeeDTO(obj);
            if (msg != null)
                return msg;
            if (obj.FullName == null || obj.UserName == null)
                return "Name/Username cannot be empty";
            if (obj.Email == null || obj.Phone == null)
                return "Email/Phone cannot be empty";
            if (_employeeRES.GetByEmail(obj.Email) != null)
                return "Email already exist";
            if (_employeeRES.GetByPhone(obj.Phone) != null)
                return "Phone already exist";
            string? avatarSrc = obj.Avatar;
            var entity = new Employee()
            {
                FullName = obj.FullName?.Trim(),
                BirthDate = obj.BirthDate.ToUniversalTime(),
                Email = obj.Email,
                Phone = obj.Phone,
                Status = obj.Status,
                RoleID = obj.RoleID,
            };
            var e = _employeeRES.AddnReturn(entity);
            if (e != null)
            {
                var acc = new Account() { UserName = e.Email, EmployeeID = e.EmployeeID, HashPassword = "123" };
                _accountRES.Add(acc);
                e.Avatar = e.EmployeeID + ".jpg";
                if (_employeeRES.Update(e))
                {
                    if (avatarSrc != null)
                        _imageHandlerSVC.CopyImage(avatarSrc, e.Avatar, true);
                    return "Add successfully";
                }
                else
                    return "Add successfully but cannot save your avatar";
            }
            if (_employeeRES.Add(entity))
                return "Add successfully";
            return "Cannot add new employee! Try later!";
        }

        public string Delete(int objID)
        {
            throw new NotImplementedException();
        }

        public List<RoleDTO> GetAllRoles()
        {
            return _roleRES.GetAll().Select(o => new RoleDTO
            {
                ID = o.RoleID, Name = o.Name,
            }).ToList();
        }

        public List<EmployeeDTO> GetAllEmployeesByName(string name)
        {
            var list1 = _employeeRES.GetByName(name);
            var list2 = _roleRES.GetAll();
            var list = from e in list1
                       join r in list2
                       on e.RoleID equals r.RoleID
                       select new EmployeeDTO()
                       {
                           ID = e.EmployeeID,
                           FullName = e.FullName,
                           BirthDate = e.BirthDate,
                           Email = e.Email,
                           Phone = e.Phone,
                           Status = e.Status,
                           Avatar = _imageHandlerSVC.GetImageDirecory(e.Avatar, true),
                           RoleName = r.Name,
                           RoleID = r.RoleID,
                           UserName = (_accountRES.GetByEmpID(e.EmployeeID))?.UserName,
                           //AvatarBitMap = _imageHandlerSVC.GetBitMap(e.Avatar, true)
                       };
            return list.ToList();
        }

        public string Update(EmployeeDTO obj)
        {
            string? msg = CheckInputData.CheckValidInpurtForEmployeeDTO(obj);
            if (msg != null) return msg;

            var updateObj = _employeeRES.GetByID(obj.ID);
            if (updateObj == null)
                return "Employee does not exist!";
            var updateAcc = _accountRES.GetByEmpID(obj.ID);
            if (obj.Email == null)
                return "Email cannot be empty";
            if (obj.Phone == null)
                return "Phone cannot be empty";
            if (obj.Email != updateObj.Email && _employeeRES.GetByEmail(obj.Email) != null)
                return "Mail already used";
            if (obj.Phone != updateObj.Phone && _employeeRES.GetByPhone(obj.Phone) != null)
                return "Phone already used";

            string? avatarSrc = obj.Avatar;

            updateObj.FullName = obj.FullName;
            updateObj.BirthDate = obj.BirthDate;
            updateObj.Email = obj.Email;
            updateObj.Phone = obj.Phone;
            updateObj.Status = obj.Status;
            updateObj.Avatar = updateObj.EmployeeID + ".jpg";
            updateObj.RoleID = obj.RoleID;
            if (_employeeRES.Update(updateObj))
            {
                if (updateAcc != null)
                {
                    updateAcc.UserName = obj.UserName;
                }
                if (avatarSrc != null)
                    _imageHandlerSVC.CopyImage(avatarSrc, updateObj.Avatar, true);
                if (updateAcc != null && !_accountRES.Update(updateAcc))
                    return "Employee information is updated but cannot update accountinformation";
                return "Update Successfully";
            }
            else
                return "Cannot update the employee";
        }
    }
}
