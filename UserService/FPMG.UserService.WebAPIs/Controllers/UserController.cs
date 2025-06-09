using FPMG.UserService.BussinessObjects.UserDTO.Response;
using FPMG.UserService.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace FPMG.UserService.WebAPIs.Controllers
{
    [ApiController]
    public class UserController : Controller
    {
        private readonly IUserRepository _userRepository;
        public UserController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        [HttpGet("api/getAllUsers")]
        public async Task<IActionResult> GetAllUsersAsync()
        {
            var users = await _userRepository.GetAllUsersAsync();
            if (users == null || !users.Any())
            {
                return NotFound("No users found.");
            }
            var userList = users.Select(u => new UserResponseDTO
            {
                UserId = u.UserId,
                UserName = u.UserName,
                Email = u.Email,
                RoleName = u.Role.RoleName,
            }).ToList();
            return Ok(userList);
        }
    }
}
