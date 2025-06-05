using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FPMG.ExamService.BussinessObjects;
using Microsoft.EntityFrameworkCore;

namespace FPMG.ExamService.DataAccessLayers
{
    public class ExamManagementDBContext : DbContext
    {
        public ExamManagementDBContext(DbContextOptions<ExamManagementDBContext> options)
            : base(options)
        {
        }
        public DbSet<Exams> Exams { get; set; }
        public DbSet<Submissions> Submissions { get; set; }
        public DbSet<Semesters> Semesters { get; set; }
    }
}
