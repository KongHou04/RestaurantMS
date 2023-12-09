using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class WorkShift
    {
        public int WorkShiftID { get; set; }

        [Required]
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
    }
}
