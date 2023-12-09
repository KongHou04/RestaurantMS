using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class OrderDTO
    {
        public int ID { get; set; }
        public DateTime OrderTime { get; set; }
        public double TotalAmount { get; set; }
        public double Incedentals { get; set; }
        public double Discount { get; set; }
        public double GrandTotal { get; set; }
        public bool Status { get; set; }
        public bool PaymentStatus { get; set; }
        public string? Description { get; set; }
        public string? TableName { get; set; }
        public string? EmployeeName { get; set; }
    }
}
