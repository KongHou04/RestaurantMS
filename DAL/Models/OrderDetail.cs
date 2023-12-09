using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace DAL.Models
{
    [Table("OrderDetails")]
    public class OrderDetail
    {
        [Key]
        public int ODID { get; set; }

        [MaxLength(255)]
        public string? ProductName { get; set; }

        [Range(0, double.MaxValue)]
        public int Quantity { get; set; }

        [Range(0, double.MaxValue)]
        public double Price { get; set; }

        [MaxLength(255)]
        public string? Description { get; set; }

        public int? ProductID { get; set; }

        public Product? Product { get; set; }

        public int? OrderID { get; set; }

        public Order? Order { get; set; }
    }
}
