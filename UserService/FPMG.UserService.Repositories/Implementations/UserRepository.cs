using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FPMG.UserService.BussinessObjects;
using FPMG.UserService.DataAccessLayers;
using FPMG.UserService.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace FPMG.UserService.Repositories.Implementations
{
    public class UserRepository : IUserRepository
    {
        private readonly UserManagementDBContext _context;
        public UserRepository(UserManagementDBContext context)
        {
            _context = context;
        }
        public async Task<List<Users>> GetAllUsersAsync()
        {
            return await _context.Users
                .Include(u => u.Role) // Include the Role navigation property
                .ToListAsync(); // Fetch all users with their roles
        }
    }
}
