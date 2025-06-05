using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FPMG.ExamService.BussinessObjects
{
    public class Submissions
    {
        [Key]
        public int SubmissionId { get; set; }
        public int ExamId { get; set; }
        public int UserId { get; set; }
        public string Content { get; set; }
        public string StudentCode { get; set; }
        public DateOnly UploadDate { get; set; }
        public DateTime SubmissionDate { get; set; }

    }
}
