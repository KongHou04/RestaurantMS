using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace DAL.Models
{
    [Table("Orders")]
    public class Order
    {
        [Key]
        public int OrderID { get; set; }

        [Required]
        public DateTime OrderTime { get; set; }

        [Range(0, double.MaxValue)]
        public double TotalAmount { get; set; }

        [Range(0, Double.MaxValue)]
        public double? Incedentals { get; set; }

        [Range(0, Double.MaxValue)]
        public double? Discount { get; set; }

        [Range(0, Double.MaxValue)]
        public double GrandTotal { get; set; }

        [Required]
        [Column(TypeName = "Boolean")]
        public bool PaymentStatus { get; set; }

        [Required]
        [Column(TypeName = "Boolean")]
        public bool Status { get; set; }

        [MaxLength(255)]
        public string? Description { get; set; }


        public int? TableID { get; set; }

        public Table? Table { get; set; }

        public int? EmployeeID { get; set; }

        public Employee? Employee { get; set; }

        public Bill? Bill { get; set; }

        public ICollection<OrderDetail>? OrderDetails { get; set; }


        public bool UpdateGrandTotal()
        {
            try
            {
                GrandTotal = TotalAmount + ((Incedentals != null) ? (double)Incedentals : 0) - ((Discount != null) ? (double)Discount : 0);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }
    }
}
