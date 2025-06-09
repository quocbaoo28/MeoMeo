using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FPMG.UserService.BussinessObjects;
using System.Threading.Tasks;

namespace FPMG.UserService.Repositories.Interfaces
{
    public interface IUserRepository
    {
        Task<List<Users>> GetAllUsersAsync();
    }
}
