using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FPMG.GradingService.BussinessObjects.DTO.Response
{
    public class PaymentResponseDTO
    {
        public int PaymentId { get; set; }
        public int GradingId { get; set; }
        public string Status { get; set; }
        public string Type { get; set; }
        public float Amount { get; set; }
        public DateOnly Date { get; set; }
    }
}
