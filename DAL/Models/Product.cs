using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace DAL.Models
{
    [Table("Products")]
    public class Product
    {
        [Key]
        public int ProductID { get; set; }

        [MaxLength(255)]
        [Required]
        public string? Name { get; set; }

        [Required]
        [Range(0, double.MaxValue)]
        public double UnitPrice { get; set; }

        [Required]
        [Column(TypeName = "Boolean")]
        public bool Status { get; set; }

        [MaxLength(255)]
        public string? Image { get; set; }

        [MaxLength(255)]
        public string? Description { get; set; }


        public int? CategoryID { get; set; }

        public Category? Category { get; set; }

        public ICollection<OrderDetail>? OrderDetails { get; set; }
    }
}
