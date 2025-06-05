using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FPMG.ExamService.BussinessObjects
{
    public class Exams
    {
        [Key]
        public int ExamId { get; set; }
        public string ExamName { get; set; }
        public ICollection<Submissions> Submissions { get; set; }
    }
}
