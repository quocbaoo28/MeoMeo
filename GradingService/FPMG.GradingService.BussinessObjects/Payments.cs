using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FPMG.GradingService.BussinessObjects
{
    public class Payments
    {
        [Key]
        public int PaymentId { get; set; }
        public int GradingId { get; set; }
        public string Status { get; set; } 
        public string Type { get; set; } 
        public float Amount { get; set; }
        public DateOnly Date { get; set; }
        public virtual Gradings Grading { get; set; }
    }
}
