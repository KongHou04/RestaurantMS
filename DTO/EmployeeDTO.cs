using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace DTODemo
{
    public class EmployeeDTO
    {
        public int ID { get; set; }
        public string? UserName { get; set; }
        public string? FullName { get; set; }
        public DateTime BirthDate { get; set; }
        public string? Email { get; set; }
        public string? Phone { get; set; }
        public string? Avatar { get; set; }
        public dynamic? AvatarBitMap { get; set; }
        public bool Status { get; set; }
        public string? RoleName { get; set; }
        public int? RoleID { get; set; }
    }
}
