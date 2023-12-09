using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class OrderDetailDTO
    {
        public int ID { get; set; }
        public string? ProductName { get; set; }
        public double UnitPrice { get; set; }
        public int Quantity { get; set; }
        public string? Description { get; set; }
        public int? OrderID { get; set; }
    }
}
