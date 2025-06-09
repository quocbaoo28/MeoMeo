using FPMG.UserService.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace FPMG.UserService.WebAPIs.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : Controller
    {
        private readonly IUserRepository _userRepository;
        public UserController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        [HttpGet("/getAllUser")]
        public async Task<IActionResult> GetAllUserAsync()
        {
            var users = await _userRepository.GetAllUsersAsync();
            if (users == null || !users.Any())
            {
                return NotFound("No users found.");
            }
            var userList = users.Select(u => new 
            {
                u.UserId,
                u.UserName,
                u.Email,
                u.Role.RoleName,
            }).ToList();
            return Ok(userList);
        }
    }
}
