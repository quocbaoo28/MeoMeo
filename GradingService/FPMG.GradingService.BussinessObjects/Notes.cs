using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FPMG.GradingService.BussinessObjects
{
    public class Notes
    {
        [Key]
        public int NoteId { get; set; }
        public int MeetingId { get; set; }
        public string Content { get; set; }
        public virtual Meetings Meeting { get; set; } // Navigation property to the associated meeting
    }
}
