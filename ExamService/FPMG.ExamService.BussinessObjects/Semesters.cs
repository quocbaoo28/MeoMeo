using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FPMG.ExamService.BussinessObjects
{
    public class Semesters
    {
        [Key]
        public int SemesterId { get; set; }
        public string SemesterName { get; set; }
        public virtual ICollection<Exams> Exam { get; set; }
    }
}
