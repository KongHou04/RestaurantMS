using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace DAL.Models
{
    [Table("Categories")]
    public class Category
    {
        [Key]
        public int CategoryID { get; set; }

        [MaxLength(255)]
        [Required]
        public string? Name { get; set; }

        [Required]
        [Column(TypeName = "Boolean")]
        public bool Status { get; set; }

        [MaxLength(255)]
        public string? Description { get; set; }


        public ICollection<Product>? Products { get; set; }
    }
}
