using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FPMG.GradingService.BussinessObjects
{
    public class Gradings
    {
        [Key]
        public int GradingId { get; set; }
        public int SubmissionId { get; set; }
        public int UserId { get; set; }
        public float Score { get; set; }
        public float? RegradeScore { get; set; }
        public string? Comment { get; set; }
        public string? Reason { get; set; }
        public string Status { get; set; }
        public string? type { get; set; }
        public ICollection<Meetings>? Meetings { get; set; } // Collection of meetings associated with the grading
    }
}
