using API_Banca.Models;
using API_Banca.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API_Banca.Controllers
{
    [ApiController]
    [Route("API/Banca/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly UserServices _userService;

        public UsersController(UserServices userService)
        {
            _userService = userService;
        }

        [HttpPost("Create")]
        public async Task<ActionResult> CreateUserAndAccount([FromBody] UserDTO userDto)
        {
            var user = new User
            {
                Name = userDto.Name,
                Birthday = userDto.Birthday,
                Gender = userDto.Gender,
                Incommes = userDto.Incommes,
            };

            var createdUser = await _userService.CreateUserAsync(user);

            return Ok(new { user.UserID,});
        }

        [HttpGet("Search/{name}")]
        public async Task<IEnumerable<User?>> GetByName(string name)
        {
            return await _userService.GetUserByNameAsync(name);
        }

    }
}

