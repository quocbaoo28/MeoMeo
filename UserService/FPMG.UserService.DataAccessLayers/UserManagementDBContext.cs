using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FPMG.UserService.BussinessObjects;
using Microsoft.EntityFrameworkCore;

namespace FPMG.UserService.DataAccessLayers
{
    public class UserManagementDBContext: DbContext
    {
        public UserManagementDBContext(DbContextOptions<UserManagementDBContext> options)
            : base(options)
        {
        }
        public DbSet<Users> Users { get; set; }
        public DbSet<Roles> Roles { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Users>()
                .HasOne(u => u.Role)
                .WithMany(r => r.Users)
                .HasForeignKey(u => u.RoleId);

            // Seed Roles
            modelBuilder.Entity<Roles>().HasData(
                new Roles { RoleId = 1, RoleName = "Head" },
                new Roles { RoleId = 2, RoleName = "Examiner" },
                new Roles { RoleId = 3, RoleName = "Lecturer" },
                new Roles { RoleId = 4, RoleName = "Student" }
            );

            // Seed Users
            modelBuilder.Entity<Users>().HasData(
                // Head
                new Users { UserId = 1, RoleId = 1, UserName = "head", Email = "head@fpmg.edu", Password = "123" },

                // Examiner
                new Users { UserId = 2, RoleId = 2, UserName = "examiner", Email = "examiner@fpmg.edu", Password = "123" },

                // Lecturers
                new Users { UserId = 3, RoleId = 3, UserName = "lecturer1", Email = "lecturer1@fpmg.edu", Password = "123" },
                new Users { UserId = 4, RoleId = 3, UserName = "lecturer2", Email = "lecturer2@fpmg.edu", Password = "123" },

                // Students
                new Users { UserId = 5, RoleId = 4, UserName = "student1", Email = "student1@fpmg.edu", Password = "123" },
                new Users { UserId = 6, RoleId = 4, UserName = "student2", Email = "student2@fpmg.edu", Password = "123" },
                new Users { UserId = 7, RoleId = 4, UserName = "student3", Email = "student3@fpmg.edu", Password = "123" },
                new Users { UserId = 8, RoleId = 4, UserName = "student4", Email = "student4@fpmg.edu", Password = "123" },
                new Users { UserId = 9, RoleId = 4, UserName = "student5", Email = "student5@fpmg.edu", Password = "123" },
                new Users { UserId = 10, RoleId = 4, UserName = "student6", Email = "student6@fpmg.edu", Password = "123" }
            );
        }

    }
}
