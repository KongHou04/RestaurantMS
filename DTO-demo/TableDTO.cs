using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class TableDTO
    {
        public int ID { get; set; }
        public string? Name { get; set; }
        public bool OrderStatus { get; set; }
        public bool Status { get; set; }
        public string? Description { get; set; }
        public string? AreaName { get; set; }
        public int? AreaID { get; set; }
    }
}
