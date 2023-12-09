using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class AreaDTO
    {
        public int ID { get; set; }
        public string? Name { get; set; }
        public int TableNumber { get; set; }
        public bool Status { get; set; }
        public string? Description { get; set; }
    }
}
