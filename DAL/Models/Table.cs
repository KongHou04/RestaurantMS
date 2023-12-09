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
    [Table("Tables")]
    public class Table
    {
        [Key]
        public int TableID { get; set; }

        [Range(0, int.MaxValue)]
        public int TableOrdByArea { get; set; }

        [Required]
        [Column(TypeName = "Boolean")]
        public bool Status { get; set; }

        [Required]
        [Column(TypeName = "Boolean")]
        public bool OrderStatus { get; set; } = false;


        public int? AreaID { get; set; }

        public Area? Area { get; set; }

        public ICollection<Order>? Orders { get; set; }
    }
}
