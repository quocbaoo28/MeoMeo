using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace FPMG.UserService.DataAccessLayers
{
    public class UserManagementDBContextFactory : IDesignTimeDbContextFactory<UserManagementDBContext>
    {
        public UserManagementDBContext CreateDbContext(string[] args)
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory()) // Đọc từ thư mục đang chạy
                .AddJsonFile("appsettings.json") // Lấy connection string từ đây
                .Build();

            var optionsBuilder = new DbContextOptionsBuilder<UserManagementDBContext>();
            var connectionString = configuration.GetConnectionString("DefaultConnection");

            optionsBuilder.UseSqlServer(connectionString);

            return new UserManagementDBContext(optionsBuilder.Options);
        }
    }
}
