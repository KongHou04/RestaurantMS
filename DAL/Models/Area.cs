using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace DAL.Models
{
    [Table("Areas")]
    public class Area
    {
        [Key]
        public int AreaID { get; set; }

        [MaxLength(255)]
        [Required]
        public string? Name { get; set; }

        [Required]
        [Column(TypeName = "Boolean")]
        public bool Status { get; set; }

        [MaxLength(255)]
        public string? Description { get; set; }


        public ICollection<Table>? Tables { get; set; }
    }
}
