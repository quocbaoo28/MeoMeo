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
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .HasOne(u => u.Role)
                .WithMany(r => r.Users)
                .HasForeignKey(u => u.RoleId);

            // Seed Roles
            modelBuilder.Entity<Role>().HasData(
                new Role { RoleId = 1, RoleName = "Head" },
                new Role { RoleId = 2, RoleName = "Examiner" },
                new Role { RoleId = 3, RoleName = "Lecturer" },
                new Role { RoleId = 4, RoleName = "Student" }
            );

            // Seed Users
            modelBuilder.Entity<User>().HasData(
                // Head
                new User { UserId = 1, RoleId = 1, UserName = "head", Email = "head@fpmg.edu", Password = "123" },

                // Examiner
                new User { UserId = 2, RoleId = 2, UserName = "examiner", Email = "examiner@fpmg.edu", Password = "123" },

                // Lecturers
                new User { UserId = 3, RoleId = 3, UserName = "lecturer1", Email = "lecturer1@fpmg.edu", Password = "123" },
                new User { UserId = 4, RoleId = 3, UserName = "lecturer2", Email = "lecturer2@fpmg.edu", Password = "123" },

                // Students
                new User { UserId = 5, RoleId = 4, UserName = "student1", Email = "student1@fpmg.edu", Password = "123" },
                new User { UserId = 6, RoleId = 4, UserName = "student2", Email = "student2@fpmg.edu", Password = "123" },
                new User { UserId = 7, RoleId = 4, UserName = "student3", Email = "student3@fpmg.edu", Password = "123" },
                new User { UserId = 8, RoleId = 4, UserName = "student4", Email = "student4@fpmg.edu", Password = "123" },
                new User { UserId = 9, RoleId = 4, UserName = "student5", Email = "student5@fpmg.edu", Password = "123" },
                new User { UserId = 10, RoleId = 4, UserName = "student6", Email = "student6@fpmg.edu", Password = "123" }
            );
        }

    }
}
