using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    [Table("Employees")]
    public class Employee
    {
        [Key]
        public int EmployeeID { get; set; }

        [MaxLength(255)]
        [Required]
        public string? FullName { get; set; }

        [Required]
        public DateTime BirthDate { get; set; }

        [MaxLength(255)]
        [Required]
        public string? Email { get; set; }

        [MaxLength(20)]
        [Required]
        public string? Phone { get; set; }

        [MaxLength(255)]
        public string? Avatar { get; set; }

        [Required]
        [Column(TypeName = "Boolean")]
        public bool Status { get; set; }


        public int? RoleID { get; set; }

        public Role? Role { get; set; }

        public Account? Account { get; set; }

        public ICollection<Order>? Orders { get; set; }
    }
}
