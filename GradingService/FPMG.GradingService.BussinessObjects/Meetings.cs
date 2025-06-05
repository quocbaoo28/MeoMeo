using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FPMG.GradingService.BussinessObjects
{
    public class Meetings
    {
        [Key]
        public int MeetingId { get; set; }
        public int GradingId { get; set; }
        public string Location { get; set; }
        public string Status { get; set; } 
        public DateTime MeetingDate { get; set; }
        public ICollection<Notes>? Notes { get; set; } // Collection of notes associated with the meeting
        public virtual Gradings Grading { get; set; } // Navigation property to the associated grading
    }
}
