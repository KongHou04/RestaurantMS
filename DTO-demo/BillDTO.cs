using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class BillDTO
    {
        public int ID { get; set; }
        public DateTime CreateTime { get; set; }
        public double GrandTotal { get; set; }
        public double Recived { get; set; }
        public double Return { get; set; }
        public string? Description { get; set; }
        public int? OrderID { get; set; }
    }
}
