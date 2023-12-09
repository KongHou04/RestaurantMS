using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace DAL.Models
{
    [Table("Bills")]
    public class Bill
    {
        [Key]
        public int BillID { get; set; }

        [Required]
        public DateTime CreateTime { get; set; }

        [Range(0, double.MaxValue)]
        public double GrandTotal { get; set; }

        [Range(0, double.MaxValue)]
        public double? Recived { get; set; }

        [Range(0, double.MaxValue)]
        public double? Return { get; set; }


        public int? OrderID { get; set; }

        public Order? Order { get; set; }
    }
}
