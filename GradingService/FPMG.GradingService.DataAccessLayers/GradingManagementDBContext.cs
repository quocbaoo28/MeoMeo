using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FPMG.GradingService.BussinessObjects;
using Microsoft.EntityFrameworkCore;

namespace FPMG.GradingService.DataAccessLayers
{
    public class GradingManagementDBContext: DbContext
    {
        public GradingManagementDBContext(DbContextOptions<GradingManagementDBContext> options)
            : base(options)
        {
        }
        public DbSet<Gradings> Gradings { get; set; }
        public DbSet<Meetings> Meetings { get; set; }
        public DbSet<Notes> Notes { get; set; }
        public DbSet<Payments> Payments { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Gradings>()
                .HasMany(g => g.Meetings)
                .WithOne(m => m.Grading)
                .HasForeignKey(m => m.GradingId);
            modelBuilder.Entity<Meetings>()
                .HasMany(m => m.Notes)
                .WithOne(n => n.Meeting)
                .HasForeignKey(n => n.MeetingId);
        }
    }
}
